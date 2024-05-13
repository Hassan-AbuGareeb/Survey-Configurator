using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using QuestionDB.models;
using Microsoft.Data.SqlClient;

namespace DatabaseLayer
{
    public class Database
    {
        public static string ConnectionString;

        private Database() { }
        #region class main functions
        public static DataTable getQuestionsFromDB()
        {
            using (SqlConnection tConn = new SqlConnection(ConnectionString)) 
            {
                tConn.Open();
                SqlCommand tGetQuestionsDataCmd = new SqlCommand("SELECT * FROM Question", tConn);
                DbDataReader tReader = tGetQuestionsDataCmd.ExecuteReader(CommandBehavior.CloseConnection);
                DataTable tQuestionsData = new DataTable();
                tQuestionsData.Load(tReader);
                tReader.Close();
                return tQuestionsData;
            }
        }

        public static DataRow getQuestionSpecificDataFromDB(int pQuestionId, string pQuestionType)
        {
            using (SqlConnection tConn = new SqlConnection(ConnectionString)) 
            {
                tConn.Open();
                SqlCommand tGetQuestionSpecificData = new SqlCommand($"SELECT * FROM {pQuestionType} WHERE Q_id = {pQuestionId}", tConn);
                DbDataReader tReader = tGetQuestionSpecificData.ExecuteReader(CommandBehavior.CloseConnection);
                DataTable tQuestionSpecificData = new DataTable();
                tQuestionSpecificData.Load(tReader);
                tReader.Close();
                return tQuestionSpecificData.Rows[0];
            }
        }

        public static int AddQuestionToDB(Question pQuestionData)
        {
            //get the Question type from its calss name
            string tQuestionType = pQuestionData.GetType().Name.Split("Q")[0];
            //create db connection
            using(SqlConnection tConn = new SqlConnection(ConnectionString))
            {
                tConn.Open();
                //insert question data in the question table
                SqlCommand tInsertQuestionCmd = new SqlCommand($"INSERT INTO Question (Q_text, Q_order, Q_type) OUTPUT INSERTED.Q_id VALUES (@Q_text, {pQuestionData.Order}, '{tQuestionType}')",
                    tConn);
                tInsertQuestionCmd.Parameters.Add(new SqlParameter("@Q_text", pQuestionData.Text));

                //insert the row data to the question and return the id of the created question
                int tQuestionId = (int)tInsertQuestionCmd.ExecuteScalar();
                    
                //get the specific values for the question type
                string tQuestionTypeSpecificAttributes = "";
                string tQuestionTypeSpecificValues = "";
                //for each type of question downcast the question to its specific type and obtain its properties
                switch (tQuestionType)
                {
                    case "Smiley":
                        SmileyQuestion smileyQuestionData = (SmileyQuestion)pQuestionData;
                        tQuestionTypeSpecificAttributes += "Num_of_faces";
                        tQuestionTypeSpecificValues += $"{smileyQuestionData.NumberOfSmileyFaces}";
                        break;
                    case "Slider":
                        SliderQuestion sliderQuestionData = (SliderQuestion)pQuestionData;
                        tQuestionTypeSpecificAttributes += "Start_value, End_value, Start_value_caption, End_value_caption";
                        tQuestionTypeSpecificValues += $"{sliderQuestionData.StartValue}, {sliderQuestionData.EndValue}," +
                            $" '{sliderQuestionData.StartValueCaption}', '{sliderQuestionData.EndValueCaption}'";
                        break;
                    case "Stars":
                        StarsQuestion starsQuestionData = (StarsQuestion)pQuestionData;
                        tQuestionTypeSpecificAttributes += "Num_of_stars";
                        tQuestionTypeSpecificValues += $"{starsQuestionData.NumberOfStars}";
                        break;
                }
                SqlCommand tInsertQuestionTypeCmd = new SqlCommand($"INSERT INTO {tQuestionType} (Q_id, {tQuestionTypeSpecificAttributes}) VALUES ({tQuestionId}, {tQuestionTypeSpecificValues})",
                    tConn);
                tInsertQuestionTypeCmd.ExecuteNonQuery();
                //return question id to add question to UI
                return tQuestionId;
            }
        }

        public static void UpdateQuestionOnDB(int pQuestionId, string pOriginalQuestionType, Question pUpdatedQuestionData)
        {
           string pUpdatedQuestionType = pUpdatedQuestionData.GetType().Name.Split("Q")[0];

           using (SqlConnection conn = new SqlConnection(ConnectionString)) 
            {
                conn.Open();
                if (pUpdatedQuestionType.Equals(pOriginalQuestionType))
                {
                    //type of question wasn't  changed
                    //update the specific details
                    string questionUpdateArguments = "";
                    SqlCommand updateQuestionSpecificDataCmd = conn.CreateCommand();
                    switch (pOriginalQuestionType)
                    {
                        case "Smiley":
                            SmileyQuestion smileyQuestionData = (SmileyQuestion)pUpdatedQuestionData;
                            questionUpdateArguments += $"Num_of_faces = {smileyQuestionData.NumberOfSmileyFaces}";
                            break;
                        case "Slider":
                            SliderQuestion sliderQuestionData = (SliderQuestion)pUpdatedQuestionData;
                            questionUpdateArguments += $"Start_value = {sliderQuestionData.StartValue}," +
                                $" End_value = {sliderQuestionData.EndValue}," +
                                $" Start_value_caption = @Start_value_caption," +
                                $" End_value_caption = @End_value_caption";
                            //to ensure safety against sql injection
                            updateQuestionSpecificDataCmd.Parameters.Add(new SqlParameter("@Start_value_caption", sliderQuestionData.StartValueCaption));
                            updateQuestionSpecificDataCmd.Parameters.Add(new SqlParameter("@End_value_caption", sliderQuestionData.EndValueCaption));
                            break;
                        case "Stars":
                            StarsQuestion starsQuestionData = (StarsQuestion)pUpdatedQuestionData;
                            questionUpdateArguments += $"Num_of_stars = {starsQuestionData.NumberOfStars}";
                            break;
                    }
                    updateQuestionSpecificDataCmd.CommandType = CommandType.Text;
                    updateQuestionSpecificDataCmd.CommandText = $"UPDATE {pOriginalQuestionType} SET {questionUpdateArguments} WHERE Q_id = {pQuestionId}";

                    //update the general question
                    SqlCommand updateQuestionDataCmd = new SqlCommand($"UPDATE Question SET Q_order = {pUpdatedQuestionData.Order}, Q_text = @Q_text WHERE Q_id = {pQuestionId}",
                        conn);
                    updateQuestionDataCmd.Parameters.Add(new SqlParameter("@Q_text", pUpdatedQuestionData.Text));

                    updateQuestionSpecificDataCmd.ExecuteNonQuery();
                    updateQuestionDataCmd.ExecuteNonQuery();
                }
                else
                {
                    //type of question changed
                    //delete the questions specific old data first
                    SqlCommand deleteSpecificQuestionDataCmd = new SqlCommand($"DELETE FROM {pOriginalQuestionType} WHERE Q_id = {pQuestionId}", conn);

                    //update the general question data
                    SqlCommand updateQuestionDataCmd = new SqlCommand
                        ($"UPDATE Question SET Q_order = {pUpdatedQuestionData.Order}, Q_text = @Q_text," +
                        $" Q_type = '{pUpdatedQuestionData.GetType().Name.Split("Q")[0]}' WHERE Q_id = {pQuestionId}",
                        conn);
                    updateQuestionDataCmd.Parameters.Add(new SqlParameter("@Q_text", pUpdatedQuestionData.Text));

                    //create a new row in the specific question type table
                    SqlCommand insertQuestionTypeCmd = conn.CreateCommand();
                    //get the specific values for the question type
                    string questionTypeSpecificAttributes = "";
                    string questionTypeSpecificValues = "";

                    //for each type of question downcast the question to its specific type
                    switch (pUpdatedQuestionType)
                    {
                        case "Smiley":
                            SmileyQuestion smileyQuestionData = (SmileyQuestion)pUpdatedQuestionData;
                            questionTypeSpecificAttributes += "Num_of_faces";
                            questionTypeSpecificValues += $"{smileyQuestionData.NumberOfSmileyFaces}";
                            break;
                        case "Slider":
                            SliderQuestion sliderQuestionData = (SliderQuestion)pUpdatedQuestionData;
                            questionTypeSpecificAttributes += "Start_value, End_value, Start_value_caption, End_value_caption";
                            questionTypeSpecificValues += $"{sliderQuestionData.StartValue}, {sliderQuestionData.EndValue}," +
                                $" @Start_value_caption, @End_value_caption";
                            //to ensure safety against sql injection
                            insertQuestionTypeCmd.Parameters.Add(new SqlParameter("@Start_value_caption", sliderQuestionData.StartValueCaption));
                            insertQuestionTypeCmd.Parameters.Add(new SqlParameter("@End_value_caption", sliderQuestionData.EndValueCaption));
                            break;
                        case "Stars":
                            StarsQuestion starsQuestionData = (StarsQuestion)pUpdatedQuestionData;
                            questionTypeSpecificAttributes += "Num_of_stars";
                            questionTypeSpecificValues += $"{starsQuestionData.NumberOfStars}";
                            break;
                    }

                    insertQuestionTypeCmd.CommandType = CommandType.Text;
                    insertQuestionTypeCmd.CommandText = $"INSERT INTO {pUpdatedQuestionType} (Q_id, {questionTypeSpecificAttributes}) VALUES ({pQuestionId}, {questionTypeSpecificValues})";

                    //exectue commands on database
                    deleteSpecificQuestionDataCmd.ExecuteNonQuery();
                    updateQuestionDataCmd.ExecuteNonQuery();
                    insertQuestionTypeCmd.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteQuestionFromDB(DataRow[] pSelectedQuestions)
        {
            using (SqlConnection tConn = new SqlConnection(ConnectionString)) 
                {
                tConn.Open();
                    SqlCommand tDeleteQuestionsCmd = tConn.CreateCommand();
                    tDeleteQuestionsCmd.CommandType = CommandType.Text;
                    //delete the specific details of the question type
                    for (int i = 0; i < pSelectedQuestions.Length; i++)
                    {
                        tDeleteQuestionsCmd.CommandText = $"DELETE FROM {pSelectedQuestions[i]["Q_type"]} WHERE Q_id = {pSelectedQuestions[i]["Q_id"]}";
                        tDeleteQuestionsCmd.ExecuteNonQuery();
                    }
                    //delete question from database
                    for (int i = 0; i < pSelectedQuestions.Length; i++)
                    {
                        tDeleteQuestionsCmd.CommandText = $"DELETE FROM Question WHERE Q_id = {pSelectedQuestions[i]["Q_id"]}";
                        tDeleteQuestionsCmd.ExecuteNonQuery();
                    }
                }
        }
        #endregion

        #region class utility functions
        public static long getChecksum()
        {
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
