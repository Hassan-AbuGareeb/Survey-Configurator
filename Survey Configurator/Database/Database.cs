using System.Data;
using System.Data.Common;
using SharedResources.models;
using Microsoft.Data.SqlClient;
using SharedResources;

namespace DatabaseLayer
{
    public class Database
    {
        public static string ConnectionString;

        private Database() { }
        #region class main functions
        public static void getQuestionsFromDB(ref List<Question> pQuestionsList)
        {
            using (SqlConnection tConn = new SqlConnection(ConnectionString)) 
            {
                tConn.Open();
                using (SqlTransaction tTransaction = tConn.BeginTransaction()) 
                { 
                    SqlCommand tGetQuestionsDataCmd = new SqlCommand("SELECT * FROM Question", tConn, tTransaction);
                    DbDataReader tReader = tGetQuestionsDataCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    //iterate over each row in the Reader and add it to the questions list
                    while (tReader.Read())
                    {
                        pQuestionsList.Add(new Question((int)tReader["Id"], tReader["Text"].ToString(),
                            (int)tReader["order"], (SharedResources.QuestionType)tReader["Type"]));
                    }
                    tReader.Close();
                }
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

                    SqlCommand tGetQuestionSpecificData = new SqlCommand($"SELECT * FROM {tQuestionType} WHERE Id = @Id", tConn, tTransaction);
                    tGetQuestionSpecificData.Parameters.Add(new SqlParameter("@Id",pQuestionData.Id));
                    DbDataReader tReader = tGetQuestionSpecificData.ExecuteReader(CommandBehavior.CloseConnection);
                    //this needs to be fixed
                    Question tSpecificQuestionData= null;
                    while(tReader.Read())
                    {
                        switch(pQuestionData.Type)
                        {
                            case QuestionType.Stars:
                                tSpecificQuestionData = new StarsQuestion(pQuestionData, (int)tReader["NumberOfStars"]);
                                break;
                            case QuestionType.Smiley:
                                tSpecificQuestionData = new SmileyQuestion(pQuestionData, (int)tReader["NumberOfFaces"]);
                                break;
                            case QuestionType.Slider:
                                tSpecificQuestionData = new SliderQuestion(pQuestionData, 
                                    (int)tReader["StartValue"], (int)tReader["EndValue"],
                                    tReader["StartValueCaption"].ToString(), tReader["EndValueCaption"].ToString());
                                break;
                        }
                    }
                    tReader.Close();
                    return tSpecificQuestionData;
                }
            }
        }

        public static Question AddQuestionToDB(Question pQuestionData)
        {
            //change to id to status code, and return id by reference

            //create db connection
            using (SqlConnection tConn = new SqlConnection(ConnectionString)) 
            {
                tConn.Open();
                using (SqlTransaction tTransaction = tConn.BeginTransaction())
                {
                    try
                    {
                        //insert question data in the question table
                        SqlCommand tInsertQuestionCmd = new SqlCommand($"INSERT INTO Question ([Text], [Order], [Type]) OUTPUT INSERTED.Id" +
                        $" VALUES (@Text, @Order, @Type)",
                        tConn, tTransaction);
                        tInsertQuestionCmd.Parameters.Add(new SqlParameter("@Text", pQuestionData.Text));
                        tInsertQuestionCmd.Parameters.Add(new SqlParameter("@Order", pQuestionData.Order));
                        tInsertQuestionCmd.Parameters.Add(new SqlParameter("@Type", pQuestionData.Type));

                        //insert the row data to the question and return the id of the created question
                        int tQuestionId = (int)tInsertQuestionCmd.ExecuteScalar();

                        //get the specific values for the question type
                        string tQuestionTypeSpecificAttributes = "";
                        string tQuestionTypeSpecificValues = "";
                        //for each type of question downcast the question to its specific type and obtain its properties
                        //make a generic function, or a specific function for each question type to make code more readable/ easier to maintain
                        switch (pQuestionData.Type)
                        {
                            case QuestionType.Stars:
                                StarsQuestion tStarsQuestionData = (StarsQuestion)pQuestionData;
                                tQuestionTypeSpecificAttributes += "NumberOfStars";
                                tQuestionTypeSpecificValues += $"{tStarsQuestionData.NumberOfStars}";
                                break;
                            case QuestionType.Smiley:
                                SmileyQuestion tSmileyQuestionData = (SmileyQuestion)pQuestionData;
                                tQuestionTypeSpecificAttributes += "NumberOfFaces";
                                tQuestionTypeSpecificValues += $"{tSmileyQuestionData.NumberOfSmileyFaces}";
                                break;
                            case QuestionType.Slider:
                                SliderQuestion tSliderQuestionData = (SliderQuestion)pQuestionData;
                                tQuestionTypeSpecificAttributes += "StartValue, EndValue, StartValueCaption, EndValueCaption";
                                tQuestionTypeSpecificValues += $"{tSliderQuestionData.StartValue}, {tSliderQuestionData.EndValue}," +
                                    $" '{tSliderQuestionData.StartValueCaption}', '{tSliderQuestionData.EndValueCaption}'";
                                break;
                        }
                        //add parameters
                        SqlCommand tInsertQuestionTypeCmd = new SqlCommand($"INSERT INTO {pQuestionData.Type} (Id, {tQuestionTypeSpecificAttributes}) VALUES ({tQuestionId}, {tQuestionTypeSpecificValues})",
                            tConn, tTransaction);
                        tInsertQuestionTypeCmd.ExecuteNonQuery();

                        //commit transaction
                        tTransaction.Commit();

                        //return question id to add question to UI
                        return new Question(tQuestionId, pQuestionData.Text, pQuestionData.Order, pQuestionData.Type);
                    }
                    catch (Exception e)
                    {
                    return null;
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
                            string tQuestionUpdateArguments = "";
                            SqlCommand tUpdateQuestionSpecificDataCmd = tConn.CreateCommand();
                            switch (pOriginalQuestionType)
                            {
                                case QuestionType.Stars:
                                    StarsQuestion tStarsQuestionData = (StarsQuestion)pUpdatedQuestionData;
                                    tQuestionUpdateArguments += $"NumberOfStars = {tStarsQuestionData.NumberOfStars}";
                                    break;
                                case QuestionType.Smiley:
                                    SmileyQuestion tSmileyQuestionData = (SmileyQuestion)pUpdatedQuestionData;
                                    tQuestionUpdateArguments += $"NumberOfFaces = {tSmileyQuestionData.NumberOfSmileyFaces}";
                                    break;
                                case QuestionType.Slider:
                                    SliderQuestion tSliderQuestionData = (SliderQuestion)pUpdatedQuestionData;
                                    tQuestionUpdateArguments += $"StartValue = {tSliderQuestionData.StartValue}," +
                                        $" EndValue = {tSliderQuestionData.EndValue}," +
                                        $" StartValueCaption = @StartValueCaption," +
                                        $" EndValuecaption = @EndValuecaption";
                                    tUpdateQuestionSpecificDataCmd.Parameters.Add(new SqlParameter("@StartValueCaption", tSliderQuestionData.StartValueCaption));
                                    tUpdateQuestionSpecificDataCmd.Parameters.Add(new SqlParameter("@EndValuecaption", tSliderQuestionData.EndValueCaption));
                                    break;
                            }
                            tUpdateQuestionSpecificDataCmd.CommandType = CommandType.Text;
                            tUpdateQuestionSpecificDataCmd.CommandText = $"UPDATE {pOriginalQuestionType} SET {tQuestionUpdateArguments} WHERE Id = {pUpdatedQuestionData.Id}";
                            tUpdateQuestionSpecificDataCmd.Transaction = tTransaction;
                            //update the general question
                            SqlCommand tUpdateQuestionDataCmd = new SqlCommand($"UPDATE Question SET [Order] = @Order, [Text] = @Text WHERE Id = {pUpdatedQuestionData.Id}",
                                tConn, tTransaction);
                            tUpdateQuestionDataCmd.Parameters.Add(new SqlParameter("@Text", pUpdatedQuestionData.Text));
                            tUpdateQuestionDataCmd.Parameters.Add(new SqlParameter("@Order", pUpdatedQuestionData.Order));

                            tUpdateQuestionSpecificDataCmd.ExecuteNonQuery();
                            tUpdateQuestionDataCmd.ExecuteNonQuery();
                        }
                        else
                        {
                            //type of question changed
                            //delete the questions specific old data first
                            SqlCommand tDeleteSpecificQuestionDataCmd = new SqlCommand($"DELETE FROM {pOriginalQuestionType} WHERE Id = {pUpdatedQuestionData.Id}", tConn, tTransaction);

                            //update the general question data
                            SqlCommand tUpdateQuestionDataCmd = new SqlCommand
                                ($"UPDATE Question SET [Order] = {pUpdatedQuestionData.Order}, [Text] = @Text," +
                                $" [Type] = '{(int)pUpdatedQuestionData.Type}' WHERE Id = {pUpdatedQuestionData.Id}",
                                tConn, tTransaction);
                            tUpdateQuestionDataCmd.Parameters.Add(new SqlParameter("@Text", pUpdatedQuestionData.Text));

                            //create a new row in the specific question type table
                            SqlCommand tInsertQuestionTypeCmd = tConn.CreateCommand();
                            //get the specific values for the question type
                            string tQuestionTypeSpecificAttributes = "";
                            string tQuestionTypeSpecificValues = "";

                            //for each type of question downcast the question to its specific type
                            switch (pUpdatedQuestionData.Type)
                            {
                                case QuestionType.Stars:
                                    StarsQuestion tStarsQuestionData = (StarsQuestion)pUpdatedQuestionData;
                                    tQuestionTypeSpecificAttributes += "NumberOfStars";
                                    tQuestionTypeSpecificValues += $"{tStarsQuestionData.NumberOfStars}";
                                    break;
                                case QuestionType.Smiley:
                                    SmileyQuestion tSmileyQuestionData = (SmileyQuestion)pUpdatedQuestionData;
                                    tQuestionTypeSpecificAttributes += "NumberOfFaces";
                                    tQuestionTypeSpecificValues += $"{tSmileyQuestionData.NumberOfSmileyFaces}";
                                    break;
                                case QuestionType.Slider:
                                    SliderQuestion tSliderQuestionData = (SliderQuestion)pUpdatedQuestionData;
                                    tQuestionTypeSpecificAttributes += "StartValue, EndValue, StartValueCaption, EndValueCaption";
                                    tQuestionTypeSpecificValues += $"{tSliderQuestionData.StartValue}, {tSliderQuestionData.EndValue}," +
                                        $" @StartValueCaption, @EndValueCaption";
                                    //to ensure safety against sql injection
                                    tInsertQuestionTypeCmd.Parameters.Add(new SqlParameter("@StartValueCaption", tSliderQuestionData.StartValueCaption));
                                    tInsertQuestionTypeCmd.Parameters.Add(new SqlParameter("@EndValueCaption", tSliderQuestionData.EndValueCaption));
                                    break;
                            }

                            tInsertQuestionTypeCmd.CommandType = CommandType.Text;
                            tInsertQuestionTypeCmd.CommandText = $"INSERT INTO {pUpdatedQuestionData.Type} (Id, {tQuestionTypeSpecificAttributes}) VALUES ({pUpdatedQuestionData.Id}, {tQuestionTypeSpecificValues})";
                            tInsertQuestionTypeCmd.Transaction = tTransaction;
                            //exectue commands on database
                            tDeleteSpecificQuestionDataCmd.ExecuteNonQuery();
                            tUpdateQuestionDataCmd.ExecuteNonQuery();
                            tInsertQuestionTypeCmd.ExecuteNonQuery();
                        }
                        tTransaction.Commit();
                    }
                    catch (Exception e)
                    {
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
                            tDeleteQuestionsCmd.CommandText = $"DELETE FROM {tCurrentQuestion.Type} WHERE Id = @Id";
                            tDeleteQuestionsCmd.Parameters.Add(new SqlParameter("@Id", tCurrentQuestion.Id));
                            tDeleteQuestionsCmd.ExecuteNonQuery();
                            tDeleteQuestionsCmd.Parameters.Clear();
                        }

                        //delete question from database
                        for (int i = 0; i < pSelectedQuestions.Count; i++)
                        {
                            Question tCurrentQuestion = pSelectedQuestions[i];
                            tDeleteQuestionsCmd.CommandText = $"DELETE FROM Question WHERE Id = @Id";
                            tDeleteQuestionsCmd.Parameters.Add(new SqlParameter("@Id", tCurrentQuestion.Id));
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
        public static long getChecksum()
        {
            //better handling for the null case
            using (SqlConnection tConn = new SqlConnection(ConnectionString))
                {
                    tConn.Open();
                    SqlCommand tGetChecksum = new SqlCommand("SELECT CHECKSUM_AGG(BINARY_CHECKSUM(*)) FROM Question WITH (NOLOCK)", tConn);
                    var tChecksum = tGetChecksum.ExecuteScalar();
                    if (DBNull.Value.Equals(tChecksum))
                        return 0;
                    return (int)tChecksum;
                }
        }
        #endregion
    }
}
