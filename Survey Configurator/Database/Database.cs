using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using DatabaseLayer.models;
using Microsoft.Data.SqlClient;

namespace DatabaseLayer
{
    public class Database
    {
        public static string ConnectionString;

        private Database() { }

        public static DataTable getQuestionsFromDB()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString)) 
            {
                conn.Open();
                SqlCommand getQuestionsDataCmd = new SqlCommand("SELECT * FROM Question",conn);
                DbDataReader reader = getQuestionsDataCmd.ExecuteReader(CommandBehavior.CloseConnection);
                DataTable QuestionsData = new DataTable();
                QuestionsData.Load(reader);
                reader.Close();
                return QuestionsData;
            }
        }

        public static DataRow getQuestionSpecificDataFromDB(int questionId, string questionType)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString)) 
            {
                conn.Open();
                SqlCommand getQuestionSpecificData = new SqlCommand($"SELECT * FROM {questionType} WHERE Q_id = {questionId}", conn);
                DbDataReader reader = getQuestionSpecificData.ExecuteReader(CommandBehavior.CloseConnection);
                DataTable QuestionSpecificData = new DataTable();
                QuestionSpecificData.Load(reader);
                reader.Close();
                return QuestionSpecificData.Rows[0];
            }
        }

        public static int AddQuestionToDB(Question questionData)
        {
            //get the Question type from its calss name
            string questionType = questionData.GetType().Name.Split("Q")[0];
            //create db connection
            using(SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                //insert question data in the question table
                SqlCommand insertQuestionCmd = new SqlCommand($"INSERT INTO Question (Q_text, Q_order, Q_type) OUTPUT INSERTED.Q_id VALUES (@Q_text, {questionData.Order}, '{questionType}')",
                    conn);
                insertQuestionCmd.Parameters.Add(new SqlParameter("@Q_text", questionData.Text));

                //insert the row data to the question and return the id of the created question
                int questionId = (int)insertQuestionCmd.ExecuteScalar();
                    
                //get the specific values for the question type
                string questionTypeSpecificAttributes = "";
                string questionTypeSpecificValues = "";
                //for each type of question downcast the question to its specific type and obtain its properties
                switch (questionType)
                {
                    case "Smiley":
                        SmileyQuestion smileyQuestionData = (SmileyQuestion)questionData;
                        questionTypeSpecificAttributes += "Num_of_faces";
                        questionTypeSpecificValues += $"{smileyQuestionData.NumberOfSmileyFaces}";
                        break;
                    case "Slider":
                        SliderQuestion sliderQuestionData = (SliderQuestion)questionData;
                        questionTypeSpecificAttributes += "Start_value, End_value, Start_value_caption, End_value_caption";
                        questionTypeSpecificValues += $"{sliderQuestionData.StartValue}, {sliderQuestionData.EndValue}," +
                            $" '{sliderQuestionData.StartValueCaption}', '{sliderQuestionData.EndValueCaption}'";
                        break;
                    case "Stars":
                        StarsQuestion starsQuestionData = (StarsQuestion)questionData;
                        questionTypeSpecificAttributes += "Num_of_stars";
                        questionTypeSpecificValues += $"{starsQuestionData.NumberOfStars}";
                        break;
                }
                SqlCommand insertQuestionTypeCmd = new SqlCommand($"INSERT INTO {questionType} (Q_id, {questionTypeSpecificAttributes}) VALUES ({questionId}, {questionTypeSpecificValues})",
                    conn);
                insertQuestionTypeCmd.ExecuteNonQuery();
                //return question id to add question to UI
                return questionId;
            }
        }

        public static void UpdateQuestionOnDB(int questionId, string originalQuestionType, Question updatedQuestionData)
        {
           string updatedQuestionType = updatedQuestionData.GetType().Name.Split("Q")[0];

           using (SqlConnection conn = new SqlConnection(ConnectionString)) 
            {
                conn.Open();
                if (updatedQuestionType.Equals(originalQuestionType))
                {
                    //type of question wasn't  changed
                    //update the specific details
                    string questionUpdateArguments = "";
                    SqlCommand updateQuestionSpecificDataCmd = conn.CreateCommand();
                    switch (originalQuestionType)
                    {
                        case "Smiley":
                            SmileyQuestion smileyQuestionData = (SmileyQuestion)updatedQuestionData;
                            questionUpdateArguments += $"Num_of_faces = {smileyQuestionData.NumberOfSmileyFaces}";
                            break;
                        case "Slider":
                            SliderQuestion sliderQuestionData = (SliderQuestion)updatedQuestionData;
                            questionUpdateArguments += $"Start_value = {sliderQuestionData.StartValue}," +
                                $" End_value = {sliderQuestionData.EndValue}," +
                                $" Start_value_caption = @Start_value_caption," +
                                $" End_value_caption = @End_value_caption";
                            //to ensure safety against sql injection
                            updateQuestionSpecificDataCmd.Parameters.Add(new SqlParameter("@Start_value_caption", sliderQuestionData.StartValueCaption));
                            updateQuestionSpecificDataCmd.Parameters.Add(new SqlParameter("@End_value_caption", sliderQuestionData.EndValueCaption));
                            break;
                        case "Stars":
                            StarsQuestion starsQuestionData = (StarsQuestion)updatedQuestionData;
                            questionUpdateArguments += $"Num_of_stars = {starsQuestionData.NumberOfStars}";
                            break;
                    }
                    updateQuestionSpecificDataCmd.CommandType = CommandType.Text;
                    updateQuestionSpecificDataCmd.CommandText = $"UPDATE {originalQuestionType} SET {questionUpdateArguments} WHERE Q_id = {questionId}";

                    //update the general question
                    SqlCommand updateQuestionDataCmd = new SqlCommand($"UPDATE Question SET Q_order = {updatedQuestionData.Order}, Q_text = @Q_text WHERE Q_id = {questionId}",
                        conn);
                    updateQuestionDataCmd.Parameters.Add(new SqlParameter("@Q_text", updatedQuestionData.Text));

                    updateQuestionSpecificDataCmd.ExecuteNonQuery();
                    updateQuestionDataCmd.ExecuteNonQuery();
                }
                else
                {
                    //type of question changed
                    //delete the questions specific old data first
                    SqlCommand deleteSpecificQuestionDataCmd = new SqlCommand($"DELETE FROM {originalQuestionType} WHERE Q_id = {questionId}", conn);

                    //update the general question data
                    SqlCommand updateQuestionDataCmd = new SqlCommand
                        ($"UPDATE Question SET Q_order = {updatedQuestionData.Order}, Q_text = @Q_text," +
                        $" Q_type = '{updatedQuestionData.GetType().Name.Split("Q")[0]}' WHERE Q_id = {questionId}",
                        conn);
                    updateQuestionDataCmd.Parameters.Add(new SqlParameter("@Q_text", updatedQuestionData.Text));

                    //create a new row in the specific question type table
                    SqlCommand insertQuestionTypeCmd = conn.CreateCommand();
                    //get the specific values for the question type
                    string questionTypeSpecificAttributes = "";
                    string questionTypeSpecificValues = "";

                    //for each type of question downcast the question to its specific type
                    switch (updatedQuestionType)
                    {
                        case "Smiley":
                            SmileyQuestion smileyQuestionData = (SmileyQuestion)updatedQuestionData;
                            questionTypeSpecificAttributes += "Num_of_faces";
                            questionTypeSpecificValues += $"{smileyQuestionData.NumberOfSmileyFaces}";
                            break;
                        case "Slider":
                            SliderQuestion sliderQuestionData = (SliderQuestion)updatedQuestionData;
                            questionTypeSpecificAttributes += "Start_value, End_value, Start_value_caption, End_value_caption";
                            questionTypeSpecificValues += $"{sliderQuestionData.StartValue}, {sliderQuestionData.EndValue}," +
                                $" @Start_value_caption, @End_value_caption";
                            //to ensure safety against sql injection
                            insertQuestionTypeCmd.Parameters.Add(new SqlParameter("@Start_value_caption", sliderQuestionData.StartValueCaption));
                            insertQuestionTypeCmd.Parameters.Add(new SqlParameter("@End_value_caption", sliderQuestionData.EndValueCaption));
                            break;
                        case "Stars":
                            StarsQuestion starsQuestionData = (StarsQuestion)updatedQuestionData;
                            questionTypeSpecificAttributes += "Num_of_stars";
                            questionTypeSpecificValues += $"{starsQuestionData.NumberOfStars}";
                            break;
                    }

                    insertQuestionTypeCmd.CommandType = CommandType.Text;
                    insertQuestionTypeCmd.CommandText = $"INSERT INTO {updatedQuestionType} (Q_id, {questionTypeSpecificAttributes}) VALUES ({questionId}, {questionTypeSpecificValues})";

                    //exectue commands on database
                    deleteSpecificQuestionDataCmd.ExecuteNonQuery();
                    updateQuestionDataCmd.ExecuteNonQuery();
                    insertQuestionTypeCmd.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteQuestionFromDB(DataRow[] selectedQuestions)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString)) 
                { 
                    conn.Open();
                    SqlCommand deleteQuestionsCmd = conn.CreateCommand();
                    deleteQuestionsCmd.CommandType = CommandType.Text;
                    //delete the specific details of the question type
                    for (int i = 0; i < selectedQuestions.Length; i++)
                    {
                        deleteQuestionsCmd.CommandText = $"DELETE FROM {selectedQuestions[i]["Q_type"]} WHERE Q_id = {selectedQuestions[i]["Q_id"]}";
                        deleteQuestionsCmd.ExecuteNonQuery();
                    }
                    //delete question from database
                    for (int i = 0; i < selectedQuestions.Length; i++)
                    {
                        deleteQuestionsCmd.CommandText = $"DELETE FROM Question WHERE Q_id = {selectedQuestions[i]["Q_id"]}";
                        deleteQuestionsCmd.ExecuteNonQuery();
                    }
                }
        }

        public static long getChecksum()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand getChecksum = new SqlCommand("SELECT CHECKSUM_AGG(BINARY_CHECKSUM(*)) FROM Question WITH (NOLOCK)", conn);
                    long checksum = (int)getChecksum.ExecuteScalar();
                    return checksum;
                }
        }
    }
}
