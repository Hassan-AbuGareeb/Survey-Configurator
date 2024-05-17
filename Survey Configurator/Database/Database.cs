﻿using System.Data;
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
                SqlCommand tGetQuestionsDataCmd = new SqlCommand("SELECT * FROM Question", tConn);
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

        public static Question getQuestionSpecificDataFromDB(Question pQuestionData)
        {
            using (SqlConnection tConn = new SqlConnection(ConnectionString)) 
            {
                tConn.Open();
                //parameterize the query
                string tQuestionType = pQuestionData.Type.ToString();

                SqlCommand tGetQuestionSpecificData = new SqlCommand($"SELECT * FROM {tQuestionType} WHERE Id = @Id", tConn);
                tGetQuestionSpecificData.Parameters.Add(new SqlParameter("@Id",pQuestionData.Id));
                DbDataReader tReader = tGetQuestionSpecificData.ExecuteReader(CommandBehavior.CloseConnection);
                Question tSpecificQuestionData= null;
                while(tReader.Read())
                {
                    switch(pQuestionData.Type)
                    {
                        case SharedData.cStarsType:
                            tSpecificQuestionData = new StarsQuestion(pQuestionData, (int)tReader["NumberOfStars"]);
                            break;
                        case SharedData.cSmileyType:
                            tSpecificQuestionData = new SmileyQuestion(pQuestionData, (int)tReader["NumberOfFaces"]);
                            break;
                        case SharedData.cSliderType:
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

        public static Question AddQuestionToDB(Question pQuestionData)
        {
            //change to id to status code, and return id by reference

            //create db connection
        using (SqlConnection tConn = new SqlConnection(ConnectionString)) {
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
                        case SharedData.cSmileyType:
                            SmileyQuestion smileyQuestionData = (SmileyQuestion)pQuestionData;
                            tQuestionTypeSpecificAttributes += "NumberOfFaces";
                            tQuestionTypeSpecificValues += $"{smileyQuestionData.NumberOfSmileyFaces}";
                            break;
                        case SharedData.cSliderType:
                            SliderQuestion sliderQuestionData = (SliderQuestion)pQuestionData;
                            tQuestionTypeSpecificAttributes += "StartValue, EndValue, StartValueCaption, EndValueCaption";
                            tQuestionTypeSpecificValues += $"{sliderQuestionData.StartValue}, {sliderQuestionData.EndValue}," +
                                $" '{sliderQuestionData.StartValueCaption}', '{sliderQuestionData.EndValueCaption}'";
                            break;
                        case SharedData.cStarsType:
                            StarsQuestion starsQuestionData = (StarsQuestion)pQuestionData;
                            tQuestionTypeSpecificAttributes += "NumberOfStars";
                            tQuestionTypeSpecificValues += $"{starsQuestionData.NumberOfStars}";
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
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

            using (SqlTransaction tTransaction = conn.BeginTransaction())
                {
                    try
                    {
                        if (pUpdatedQuestionData.Type.Equals(pOriginalQuestionType))
                        {
                            //type of question wasn't  changed
                            //update the specific details
                            string questionUpdateArguments = "";
                            SqlCommand updateQuestionSpecificDataCmd = conn.CreateCommand();
                            switch (pOriginalQuestionType)
                            {
                                case SharedData.cSmileyType:
                                    SmileyQuestion smileyQuestionData = (SmileyQuestion)pUpdatedQuestionData;
                                    questionUpdateArguments += $"NumberOfFaces = {smileyQuestionData.NumberOfSmileyFaces}";
                                    break;
                                case SharedData.cSliderType:
                                    SliderQuestion sliderQuestionData = (SliderQuestion)pUpdatedQuestionData;
                                    questionUpdateArguments += $"StartValue = {sliderQuestionData.StartValue}," +
                                        $" EndValue = {sliderQuestionData.EndValue}," +
                                        $" StartValueCaption = @StartValueCaption," +
                                        $" EndValuecaption = @EndValuecaption";

                                    updateQuestionSpecificDataCmd.Parameters.Add(new SqlParameter("@StartValueCaption", sliderQuestionData.StartValueCaption));
                                    updateQuestionSpecificDataCmd.Parameters.Add(new SqlParameter("@EndValuecaption", sliderQuestionData.EndValueCaption));
                                    break;
                                case SharedData.cStarsType:
                                    StarsQuestion starsQuestionData = (StarsQuestion)pUpdatedQuestionData;
                                    questionUpdateArguments += $"NumberOfStars = {starsQuestionData.NumberOfStars}";
                                    break;
                            }
                            updateQuestionSpecificDataCmd.CommandType = CommandType.Text;
                            updateQuestionSpecificDataCmd.CommandText = $"UPDATE {pOriginalQuestionType} SET {questionUpdateArguments} WHERE Id = {pUpdatedQuestionData.Id}";
                            updateQuestionSpecificDataCmd.Transaction = tTransaction;
                            //update the general question
                            SqlCommand updateQuestionDataCmd = new SqlCommand($"UPDATE Question SET [Order] = @Order, [Text] = @Text WHERE Id = {pUpdatedQuestionData.Id}",
                                conn, tTransaction);
                            updateQuestionDataCmd.Parameters.Add(new SqlParameter("@Text", pUpdatedQuestionData.Text));
                            updateQuestionDataCmd.Parameters.Add(new SqlParameter("@Order", pUpdatedQuestionData.Order));

                            updateQuestionSpecificDataCmd.ExecuteNonQuery();
                            updateQuestionDataCmd.ExecuteNonQuery();
                        }
                        else
                        {
                            //type of question changed
                            //delete the questions specific old data first
                            SqlCommand deleteSpecificQuestionDataCmd = new SqlCommand($"DELETE FROM {pOriginalQuestionType} WHERE Id = {pUpdatedQuestionData.Id}", conn, tTransaction);

                            //update the general question data
                            SqlCommand updateQuestionDataCmd = new SqlCommand
                                ($"UPDATE Question SET [Order] = {pUpdatedQuestionData.Order}, [Text] = @Text," +
                                $" [Type] = '{(int)pUpdatedQuestionData.Type}' WHERE Id = {pUpdatedQuestionData.Id}",
                                conn, tTransaction);
                            updateQuestionDataCmd.Parameters.Add(new SqlParameter("@Text", pUpdatedQuestionData.Text));

                            //create a new row in the specific question type table
                            SqlCommand insertQuestionTypeCmd = conn.CreateCommand();
                            //get the specific values for the question type
                            string questionTypeSpecificAttributes = "";
                            string questionTypeSpecificValues = "";

                            //for each type of question downcast the question to its specific type
                            switch (pUpdatedQuestionData.Type)
                            {
                                case SharedData.cSmileyType:
                                    SmileyQuestion smileyQuestionData = (SmileyQuestion)pUpdatedQuestionData;
                                    questionTypeSpecificAttributes += "NumberOfFaces";
                                    questionTypeSpecificValues += $"{smileyQuestionData.NumberOfSmileyFaces}";
                                    break;
                                case SharedData.cSliderType:
                                    SliderQuestion sliderQuestionData = (SliderQuestion)pUpdatedQuestionData;
                                    questionTypeSpecificAttributes += "StartValue, EndValue, StartValueCaption, EndValueCaption";
                                    questionTypeSpecificValues += $"{sliderQuestionData.StartValue}, {sliderQuestionData.EndValue}," +
                                        $" @StartValueCaption, @EndValueCaption";
                                    //to ensure safety against sql injection
                                    insertQuestionTypeCmd.Parameters.Add(new SqlParameter("@StartValueCaption", sliderQuestionData.StartValueCaption));
                                    insertQuestionTypeCmd.Parameters.Add(new SqlParameter("@EndValueCaption", sliderQuestionData.EndValueCaption));
                                    break;
                                case SharedData.cStarsType:
                                    StarsQuestion starsQuestionData = (StarsQuestion)pUpdatedQuestionData;
                                    questionTypeSpecificAttributes += "NumberOfStars";
                                    questionTypeSpecificValues += $"{starsQuestionData.NumberOfStars}";
                                    break;
                            }

                            insertQuestionTypeCmd.CommandType = CommandType.Text;
                            insertQuestionTypeCmd.CommandText = $"INSERT INTO {pUpdatedQuestionData.Type} (Id, {questionTypeSpecificAttributes}) VALUES ({pUpdatedQuestionData.Id}, {questionTypeSpecificValues})";
                            insertQuestionTypeCmd.Transaction = tTransaction;
                            //exectue commands on database
                            deleteSpecificQuestionDataCmd.ExecuteNonQuery();
                            updateQuestionDataCmd.ExecuteNonQuery();
                            insertQuestionTypeCmd.ExecuteNonQuery();
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

        public static void DeleteQuestionFromDB(Question[] pSelectedQuestions)
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
                        for (int i = 0; i < pSelectedQuestions.Length; i++)
                        {
                            Question tCurrentQuestion = pSelectedQuestions[i];
                            tDeleteQuestionsCmd.CommandText = $"DELETE FROM {tCurrentQuestion.Type} WHERE Id = @Id";
                            tDeleteQuestionsCmd.Parameters.Add(new SqlParameter("@Id", tCurrentQuestion.Id));
                            tDeleteQuestionsCmd.ExecuteNonQuery();
                            tDeleteQuestionsCmd.Parameters.Clear();
                        }

                        //delete question from database
                        for (int i = 0; i < pSelectedQuestions.Length; i++)
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
