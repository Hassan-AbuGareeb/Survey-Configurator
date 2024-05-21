using System.Data;
using System.Data.Common;
using SharedResources.models;
using Microsoft.Data.SqlClient;
using SharedResources;
using System.Transactions;
using System;

namespace DatabaseLayer
{
    public class Database
    {
        public static string ConnectionString;

        //constants
            //Question table constants
        private const string cQuestionsTableName = "Question";
        private const string cIdColumn = "Id";
        private const string cTextColumn = "Text";
        private const string cOrderColumn = "Order";
        private const string cTypeColumn = "Type";
        //specific types constants
            //Stars table
        private const string cNumberOfStarsColumn = "NumberOfStars";
            //Smiley table
        private const string cNumberOfFacesColumn = "NumberOfFaces";
            //Slider table
        private const string cStartValueColumn = "StartValue";
        private const string cEndValueColumn = "EndValue";
        private const string cStartValueCaptionColumn = "StartValueCaption";
        private const string cEndValueCaptionColumn = "EndValueCaption";

        private Database() { }
        #region class main functions

        public static OperationResult TestDataBaseConnection()
        {
            try
            {
                SqlConnection tConn = new SqlConnection(ConnectionString);
                tConn.Open();
                tConn.Close();
                return new OperationResult();
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
                return new OperationResult(ErrorTypes.UnknownError, "Unable to connect to database, please refer to your system admin");
            }
        }

        public static OperationResult getQuestionsFromDB(ref List<Question> pQuestionsList)
        {
            try 
            { 
                using (SqlConnection tConn = new SqlConnection(ConnectionString)) 
                {
                    tConn.Open();
                    using (SqlTransaction tTransaction = tConn.BeginTransaction()) 
                    { 
                        SqlCommand tGetQuestionsDataCmd = new SqlCommand($"SELECT * FROM [{cQuestionsTableName}]", tConn, tTransaction);
                        DbDataReader tReader = tGetQuestionsDataCmd.ExecuteReader(CommandBehavior.CloseConnection);
                        //iterate over each row in the Reader and add it to the questions list
                        while (tReader.Read())
                        {
                            pQuestionsList.Add(new Question((int)tReader[cIdColumn], tReader[cTextColumn].ToString(),
                                (int)tReader[cOrderColumn], (QuestionType)tReader[cTypeColumn]));
                        }
                        tReader.Close();
                    }

                }
                return new OperationResult();
            }
            catch(SqlException ex)
            {
                UtilityMethods.LogError(ex);
                return new OperationResult(ErrorTypes.SqlError, "An error occured while getting data, try again and if this error persists try restarting the app");
            }
            catch(Exception ex)
            {
                UtilityMethods.LogError(ex);
                return new OperationResult(ErrorTypes.UnknownError, "An unknown error occured");
            }
        }

        public static Question getQuestionSpecificDataFromDB(Question pQuestionData)
        {
            using (SqlConnection tConn = new SqlConnection(ConnectionString)) 
            {
                tConn.Open();
                using (SqlTransaction tTransaction = tConn.BeginTransaction()) 
                { 
                    //parameterize the query
                    string tQuestionType = pQuestionData.Type.ToString();

                    SqlCommand tGetQuestionSpecificData = new SqlCommand(
                        $"SELECT * FROM " +
                        $"[{tQuestionType}] " +
                        $"WHERE [{cIdColumn}] = @{cIdColumn}", tConn, tTransaction);
                    tGetQuestionSpecificData.Parameters.Add(new SqlParameter($"@{cIdColumn}",pQuestionData.Id));
                    DbDataReader tReader = tGetQuestionSpecificData.ExecuteReader(CommandBehavior.CloseConnection);
                    //this needs to be fixed
                    Question tSpecificQuestionData= null;
                    while(tReader.Read())
                    {
                        switch(pQuestionData.Type)
                        {
                            case QuestionType.Stars:
                                tSpecificQuestionData = new StarsQuestion(pQuestionData, (int)tReader[cNumberOfStarsColumn]);
                                break;
                            case QuestionType.Smiley:
                                tSpecificQuestionData = new SmileyQuestion(pQuestionData, (int)tReader[cNumberOfFacesColumn]);
                                break;
                            case QuestionType.Slider:
                                tSpecificQuestionData = new SliderQuestion(pQuestionData, 
                                    (int)tReader[cStartValueColumn], (int)tReader[cEndValueColumn],
                                    tReader[cStartValueCaptionColumn].ToString(), tReader[cEndValueCaptionColumn ].ToString());
                                break;
                        }
                    }
                    tReader.Close();
                    return tSpecificQuestionData;
                }
            }
        }

        public static int AddQuestionToDB(ref Question pQuestionData)
        {
            //change to id to status code, and return id by reference
            //no need to return the question ID just add it to the question data
            //create db connection
            using (SqlConnection tConn = new SqlConnection(ConnectionString)) 
            {
                tConn.Open();
                using (SqlTransaction tTransaction = tConn.BeginTransaction())
                {
                    try
                    {
                        //insert question data in the question table
                        SqlCommand tInsertQuestionCmd = new SqlCommand(
                            $"INSERT INTO {cQuestionsTableName} ([{cTextColumn}], [{cOrderColumn}], [{cTypeColumn}]) " +
                            $"OUTPUT INSERTED.Id " +
                            $"VALUES (@{cTextColumn}, @{cOrderColumn}, @{cTypeColumn})",
                            tConn, tTransaction);
                        tInsertQuestionCmd.Parameters.AddRange(
                        [
                            new SqlParameter($"@{cTextColumn}", pQuestionData.Text),
                            new SqlParameter($"@{cOrderColumn}", pQuestionData.Order),
                            new SqlParameter($"@{cTypeColumn}", pQuestionData.Type)
                        ]);

                        //insert the row data to the question and return the id of the created question
                        int tQuestionId = (int)tInsertQuestionCmd.ExecuteScalar();

                        //add the question Id to the quesiton data
                        pQuestionData.Id = tQuestionId;

                        //for each type of question downcast the question to its specific type and obtain its properties
                        //make a generic function, or a specific function for each question type to make code more readable/ easier to maintain
                        SqlCommand tInsertQuestionTypeCmd = null;
                        switch (pQuestionData.Type)
                        {
                            case QuestionType.Stars:
                                tInsertQuestionTypeCmd = getAddStarsCommand(tQuestionId, (StarsQuestion)pQuestionData);
                                break;
                            case QuestionType.Smiley:
                                tInsertQuestionTypeCmd = getAddSmileyCommand(tQuestionId, (SmileyQuestion)pQuestionData);
                                break;
                            case QuestionType.Slider:
                                tInsertQuestionTypeCmd = getAddSliderCommand(tQuestionId, (SliderQuestion)pQuestionData);
                                break;
                        }
                        //add parameters
                        tInsertQuestionTypeCmd.Connection = tConn;
                        tInsertQuestionTypeCmd.Transaction = tTransaction;
                        tInsertQuestionTypeCmd.ExecuteNonQuery();
                        //commit transaction
                        tTransaction.Commit();
                        return 0;
                    }
                    catch (Exception e)
                    {
                        tTransaction.Rollback();
                        UtilityMethods.LogError(e);
                        return -1;
                    }

                }
            }
        }

        public static void UpdateQuestionOnDB(QuestionType pOriginalQuestionType, Question pUpdatedQuestionData)
        {
            using (SqlConnection tConn = new SqlConnection(ConnectionString))
            {
                tConn.Open();
                using (SqlTransaction tTransaction = tConn.BeginTransaction())
                {
                    try
                    {
                        if (pUpdatedQuestionData.Type.Equals(pOriginalQuestionType))
                        {
                            //type of question wasn't  changed
                            //update the specific details
                            SqlCommand tUpdateQuestionSpecificDataCmd = null;
                            switch (pOriginalQuestionType)
                            {
                                case QuestionType.Stars:
                                    tUpdateQuestionSpecificDataCmd = getUpdateStarsCommand(pUpdatedQuestionData.Id, (StarsQuestion)pUpdatedQuestionData);
                                    break;
                                case QuestionType.Smiley:
                                    tUpdateQuestionSpecificDataCmd = getUpdateSmileyCommand(pUpdatedQuestionData.Id,(SmileyQuestion)pUpdatedQuestionData);
                                    break;
                                case QuestionType.Slider:
                                    tUpdateQuestionSpecificDataCmd = getUpdateSliderCommand(pUpdatedQuestionData.Id, (SliderQuestion)pUpdatedQuestionData);
                                    break;
                            }
                            tUpdateQuestionSpecificDataCmd.Connection = tConn;
                            tUpdateQuestionSpecificDataCmd.Transaction = tTransaction;
                            tUpdateQuestionSpecificDataCmd.ExecuteNonQuery();
                        }
                        else
                        {
                            //type of question changed
                            //delete the questions specific old data first
                            SqlCommand tDeleteSpecificQuestionDataCmd = new SqlCommand(
                                $"DELETE FROM {pOriginalQuestionType} " +
                                $"WHERE [{cIdColumn}] = @{cIdColumn}",
                                tConn, tTransaction);
                            tDeleteSpecificQuestionDataCmd.Parameters.Add(new SqlParameter($@"{cIdColumn}",pUpdatedQuestionData.Id));
                            
                            //create a new row in the specific question type table
                            SqlCommand tInsertQuestionTypeCmd = null;

                            //for each type of question downcast the question to its specific type
                            switch (pUpdatedQuestionData.Type)
                            {
                                case QuestionType.Stars:
                                    tInsertQuestionTypeCmd = getAddStarsCommand(pUpdatedQuestionData.Id, (StarsQuestion)pUpdatedQuestionData);
                                    break;
                                case QuestionType.Smiley:
                                    tInsertQuestionTypeCmd = getAddSmileyCommand(pUpdatedQuestionData.Id, (SmileyQuestion)pUpdatedQuestionData);
                                    break;
                                case QuestionType.Slider:
                                    tInsertQuestionTypeCmd = getAddSliderCommand(pUpdatedQuestionData.Id, (SliderQuestion)pUpdatedQuestionData);
                                    break;
                            }
                            tInsertQuestionTypeCmd.Connection = tConn;
                            tInsertQuestionTypeCmd.Transaction = tTransaction;
                            //exectue commands on database
                            tDeleteSpecificQuestionDataCmd.ExecuteNonQuery();
                            tInsertQuestionTypeCmd.ExecuteNonQuery();
                        }

                        //set the data type based on whether it changed or not
                        int pUpdatedQuestionType = pOriginalQuestionType == pUpdatedQuestionData.Type ? (int)pOriginalQuestionType : (int)pUpdatedQuestionData.Type;

                        //update the general data of the question after changing the specific data
                        SqlCommand tUpdateQuestionDataCmd = new SqlCommand(
                            $"UPDATE {cQuestionsTableName} SET" +
                            $" [{cOrderColumn}] = @{cOrderColumn}," +
                            $" [{cTextColumn}] = @{cTextColumn}, " +
                            $" [{cTypeColumn}] = @{cTypeColumn}" +
                            $" WHERE [{cIdColumn}] = @{cIdColumn}",
                               tConn, tTransaction);
                        //add parameters to the query
                        tUpdateQuestionDataCmd.Parameters.AddRange(
                        [
                            new SqlParameter($"@{cIdColumn}", pUpdatedQuestionData.Id),
                            new SqlParameter($"@{cTextColumn}", pUpdatedQuestionData.Text),
                            new SqlParameter($"@{cOrderColumn}", pUpdatedQuestionData.Order),
                            new SqlParameter($"@{cTypeColumn}", pUpdatedQuestionType)
                        ]);
                        tUpdateQuestionDataCmd.ExecuteNonQuery();

                        tTransaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        UtilityMethods.LogError(ex);
                        tTransaction.Rollback();
                    }
                }
            }

        }

        public static void DeleteQuestionFromDB(List<Question> pSelectedQuestions)
        {
            using (SqlConnection tConn = new SqlConnection(ConnectionString))
            {
                tConn.Open();
                using (SqlTransaction tTransaction = tConn.BeginTransaction())
                {
                    try 
                    {
                        SqlCommand tDeleteQuestionsCmd = tConn.CreateCommand();
                        tDeleteQuestionsCmd.CommandType = CommandType.Text;
                        tDeleteQuestionsCmd.Transaction = tTransaction;
                        //delete the specific details of the question type
                        for (int i = 0; i < pSelectedQuestions.Count; i++)
                        {
                            Question tCurrentQuestion = pSelectedQuestions[i];
                            tDeleteQuestionsCmd.CommandText = 
                                $"DELETE FROM {tCurrentQuestion.Type} " +
                                $"WHERE [{cIdColumn}] = @{cIdColumn}";
                            tDeleteQuestionsCmd.Parameters.Add(new SqlParameter($"@{cIdColumn}", tCurrentQuestion.Id));
                            tDeleteQuestionsCmd.ExecuteNonQuery();
                            tDeleteQuestionsCmd.Parameters.Clear();
                        }

                        //delete question from database
                        for (int i = 0; i < pSelectedQuestions.Count; i++)
                        {
                            Question tCurrentQuestion = pSelectedQuestions[i];
                            tDeleteQuestionsCmd.CommandText = 
                                $"DELETE FROM {cQuestionsTableName} " +
                                $"WHERE [{cIdColumn}] = @{cIdColumn}";
                            tDeleteQuestionsCmd.Parameters.Add(new SqlParameter($"@{cIdColumn}", tCurrentQuestion.Id));
                            tDeleteQuestionsCmd.ExecuteNonQuery();
                            tDeleteQuestionsCmd.Parameters.Clear();
                        }
                        tTransaction.Commit();
                    }
                    catch(Exception ex)
                    {
                        tTransaction.Rollback();
                    }
                }
            }
        }
        #endregion

        #region class utility functions
        private static SqlCommand getAddStarsCommand(int pQuestionId, StarsQuestion pStarsQuestionData)
        {
            SqlCommand tInsertQuestionTypeCmd = new SqlCommand(
                $"INSERT INTO {QuestionType.Stars} " +
                $"([{cIdColumn}], [{cNumberOfStarsColumn}]) " +
                $"VALUES ( @{cIdColumn}, @{cNumberOfStarsColumn} )");

            tInsertQuestionTypeCmd.Parameters.AddRange([
                new SqlParameter($"@{cIdColumn}", pQuestionId),
                new SqlParameter($"@{cNumberOfStarsColumn}", pStarsQuestionData.NumberOfStars)
                ]);
            return tInsertQuestionTypeCmd;

        }
        private static SqlCommand getAddSmileyCommand(int pQuestionId, SmileyQuestion pSmileyQuestionData)
        {
            SqlCommand tInsertQuestionTypeCmd = new SqlCommand(
                $"INSERT INTO {QuestionType.Smiley} " +
                $"([{cIdColumn}], [{cNumberOfFacesColumn}]) " +
                $"VALUES ( @{cIdColumn}, @{cNumberOfFacesColumn} )");

            tInsertQuestionTypeCmd.Parameters.AddRange([
                new SqlParameter($"@{cIdColumn}", pQuestionId),
                new SqlParameter($"@{cNumberOfFacesColumn}", pSmileyQuestionData.NumberOfSmileyFaces)
                ]);
            return tInsertQuestionTypeCmd;

        }
        private static SqlCommand getAddSliderCommand(int pQuestionId, SliderQuestion pSliderQuestionData)
        {
            SqlCommand tInsertQuestionTypeCmd = new SqlCommand(
                $"INSERT INTO {QuestionType.Slider} " +
                $"([{cIdColumn}], [{cStartValueColumn}],[{cEndValueColumn}],[{cStartValueCaptionColumn}],[{cEndValueCaptionColumn}]) " +
                $"VALUES (@{cIdColumn}, @{cStartValueColumn}, @{cEndValueColumn}, @{cStartValueCaptionColumn}, @{cEndValueCaptionColumn})");

            tInsertQuestionTypeCmd.Parameters.AddRange([
                new SqlParameter($"@{cIdColumn}", pQuestionId),
                new SqlParameter($"@{cStartValueColumn}", pSliderQuestionData.StartValue),
                new SqlParameter($"@{cEndValueColumn}", pSliderQuestionData.EndValue),
                new SqlParameter($"@{cStartValueCaptionColumn}", pSliderQuestionData.StartValueCaption),
                new SqlParameter($"@{cEndValueCaptionColumn}", pSliderQuestionData.EndValueCaption)
                ]);
            return tInsertQuestionTypeCmd;
        }

        private static SqlCommand getUpdateStarsCommand(int pQuestionId, StarsQuestion pStarsQuestionData)
        {
            SqlCommand tUpdateQuestionSpecificDataCmd = new SqlCommand(
                $"UPDATE {QuestionType.Stars} SET " +
                $"[{cNumberOfStarsColumn}] = @{cNumberOfStarsColumn} " +
                $"WHERE [{cIdColumn}] = @{cIdColumn}");

            tUpdateQuestionSpecificDataCmd.Parameters.AddRange([
                new SqlParameter($"@{cIdColumn}", pQuestionId),
                new SqlParameter($"@{cNumberOfStarsColumn}", pStarsQuestionData.NumberOfStars)
                ]);
            return tUpdateQuestionSpecificDataCmd;

        }
        private static SqlCommand getUpdateSmileyCommand(int pQuestionId, SmileyQuestion pSmileyQuestionData)
        {
            SqlCommand tUpdateQuestionSpecificDataCmd = new SqlCommand(
                $"UPDATE {QuestionType.Smiley} SET " +
                $"[{cNumberOfFacesColumn}] = @{cNumberOfFacesColumn} " +
                $"WHERE [{cIdColumn}] = @{cIdColumn}");

            tUpdateQuestionSpecificDataCmd.Parameters.AddRange([
                new SqlParameter($"@{cIdColumn}", pQuestionId),
                new SqlParameter($"@{cNumberOfFacesColumn}", pSmileyQuestionData.NumberOfSmileyFaces)
                ]);
            return tUpdateQuestionSpecificDataCmd;

        }
        private static SqlCommand getUpdateSliderCommand(int pQuestionId, SliderQuestion pStarsQuestionData)
        {
            SqlCommand tUpdateQuestionSpecificDataCmd = new SqlCommand(
                $"UPDATE {QuestionType.Slider} SET " +
                $"[{cStartValueColumn}] = @{cStartValueColumn}, " +
                $"[{cEndValueColumn}] = @{cEndValueColumn}, " +
                $"[{cStartValueCaptionColumn}] = @{cStartValueCaptionColumn}, " +
                $"[{cEndValueCaptionColumn}] = @{cEndValueCaptionColumn} " +
                $"WHERE [{cIdColumn}] = @{cIdColumn}");

            tUpdateQuestionSpecificDataCmd.Parameters.AddRange([
                new SqlParameter($"@{cIdColumn}", pQuestionId),
                new SqlParameter($"@{cStartValueColumn}", pStarsQuestionData.StartValue),
                new SqlParameter($"@{cEndValueColumn}", pStarsQuestionData.EndValue),
                new SqlParameter($"@{cStartValueCaptionColumn}", pStarsQuestionData.StartValueCaption),
                new SqlParameter($"@{cEndValueCaptionColumn}", pStarsQuestionData.EndValueCaption)
                ]);
            return tUpdateQuestionSpecificDataCmd;
        }

        public static OperationResult getChecksum(ref long pChecksum)
        {
            //better handling for the null case
            try { 
            using (SqlConnection tConn = new SqlConnection(ConnectionString))
                {
                    tConn.Open();
                    SqlCommand tGetChecksum = new SqlCommand($"SELECT CHECKSUM_AGG(BINARY_CHECKSUM(*)) FROM {cQuestionsTableName} WITH (NOLOCK)", tConn);
                    var tChecksum = tGetChecksum.ExecuteScalar();
                    //handle this in a better way
                    if (DBNull.Value.Equals(tChecksum))
                        return new OperationResult(ErrorTypes.SqlError, "Database was just created");
                    pChecksum = (int)tChecksum;
                    return new OperationResult();
                }
            } 
            catch(Exception ex)
            {
                UtilityMethods.LogError(ex);
                return new OperationResult(ErrorTypes.UnknownError, "An Unkown error occured");
            }
        }
        #endregion
    }
}
