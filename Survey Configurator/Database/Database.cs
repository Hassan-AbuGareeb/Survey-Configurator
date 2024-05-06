using System;
using System.Data;
using System.Data.Common;
using DatabaseLayer.models;
using Microsoft.Data.SqlClient;

namespace DatabaseLayer
{
    public class Database
    {
        public static string ConnectionString;
        private static DataTable Questions = new DataTable();

        private Database() { }

        public static DataTable getQuestionsFromDB()
        {
            try { 
                using (SqlConnection conn = new SqlConnection(ConnectionString)) 
                {
                    DbDataReader reader;
                    conn.Open();
                    SqlCommand getQuestionsDataCmd = new SqlCommand("SELECT * FROM Question",conn);
                    reader = getQuestionsDataCmd.ExecuteReader();
                    Questions.Load(reader);
                    return Questions;
                }
            }catch(SqlException ex)
            {
                //log error
                throw ;
            }catch(Exception ex)
            {
                //log error
                throw ;
            }
        }

        public static DataRow getQuestionData(int questionId)
        {
            DataRow questionGeneralData = Questions.Select($"Q_id = {questionId}")[0];
            return questionGeneralData;
        }

        public static DataRow getQuestionSpecificDataFromDB(int questionId, string questionType)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString)) 
            {
                DbDataReader reader;
                conn.Open();
                SqlCommand getQuestionSpecificData = new SqlCommand($"SELECT * FROM {questionType} WHERE Q_id = {questionId}", conn);
                reader = getQuestionSpecificData.ExecuteReader();
                DataTable tempTable = new DataTable();
                tempTable.Load(reader);
                reader.Close();
                return tempTable.Rows[0];
            }
        }

        public static void AddQuestionToDB(Question questionData)
        {
            //get the Question type from its calss name
            string questionType = questionData.GetType().Name.Split("Q")[0];
            //create db connection
            using(SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                //insert question data in the question table
                SqlCommand insertQuestionCmd = new SqlCommand($"INSERT INTO Question (Q_text, Q_order, Q_type) OUTPUT INSERTED.Q_id VALUES (@Q_text, {questionData.Order}, '{questionType}')"
                    , conn);
                insertQuestionCmd.Parameters.Add(new SqlParameter("@Q_text", questionData.Text));

                //insert the row data to the question and return the id of the created question
                int questionId = (int)insertQuestionCmd.ExecuteScalar();

                SqlCommand insertQuestionTypeCmd = conn.CreateCommand();
                //get the specific values for the question type
                string questionTypeSpecificAttributes = "";
                string questionTypeSpecificValues = "";
                //for each type of question downcast the question to its specific type
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
                insertQuestionTypeCmd.CommandType = CommandType.Text;
                insertQuestionTypeCmd.CommandText = $"INSERT INTO {questionType} (Q_id, {questionTypeSpecificAttributes}) VALUES ({questionId}, {questionTypeSpecificValues})";
                insertQuestionTypeCmd.ExecuteNonQuery();
                //add question to ui
                Questions.Rows.Add(questionId, questionData.Text, questionData.Order, questionType);
            }
        }

        public static void UpdateQuestionOnDB(int questionId, Question updatedQuestionData)
        {
            //get the original question type
            string originalQuestionType = Questions.Select($"Q_id = {questionId}")[0]["Q_type"].ToString();
            string updatedQuestionType = updatedQuestionData.GetType().Name.Split("Q")[0];
            using (SqlConnection conn = new SqlConnection(ConnectionString)) 
            {
                conn.Open();
                if (updatedQuestionType.Equals(originalQuestionType))
                {
                    //type of question wasn't  changed
                    //update the specific details
                    string questionUpdateArguments = "";
                    switch (originalQuestionType)
                    {
                        case "Smiley":
                            SmileyQuestion smileyQuestionData = (SmileyQuestion)updatedQuestionData;
                            questionUpdateArguments += $"Num_of_faces = {smileyQuestionData.NumberOfSmileyFaces}";
                            break;
                        case "Slider":
                            SliderQuestion sliderQuestionData = (SliderQuestion)updatedQuestionData;
                            questionUpdateArguments += $"Start_value = {sliderQuestionData.StartValue}," +
                                $" End_value ={sliderQuestionData.EndValue}," +
                                $" Start_value_caption = '{sliderQuestionData.StartValueCaption}'," +
                                $" End_value_caption = '{sliderQuestionData.EndValueCaption}'";
                            break;
                        case "Stars":
                            StarsQuestion starsQuestionData = (StarsQuestion)updatedQuestionData;
                            questionUpdateArguments += $"Num_of_stars = {starsQuestionData.NumberOfStars}";
                            break;
                    }
                    SqlCommand updateQuestionSpecificDataCmd = new SqlCommand();
                    updateQuestionSpecificDataCmd.CommandType = CommandType.Text;
                    updateQuestionSpecificDataCmd.CommandText = $"UPDATE {originalQuestionType} SET {questionUpdateArguments} WHERE Q_id = {questionId}";
                    updateQuestionSpecificDataCmd.Connection = conn;
                    //update the general question
                    SqlCommand updateQuestionDataCmd = conn.CreateCommand();
                    updateQuestionDataCmd.CommandType = CommandType.Text;
                    updateQuestionDataCmd.CommandText = $"UPDATE Question SET Q_order = {updatedQuestionData.Order}, Q_text = @Q_text WHERE Q_id = {questionId}";
                    updateQuestionDataCmd.Parameters.Add(new SqlParameter("@Q_text", updatedQuestionData.Text));
                    updateQuestionDataCmd.Connection = conn;
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
                    ////get the specific values for the question type
                    string questionTypeSpecificAttributes = "";
                    string questionTypeSpecificValues = "";
                    ////for each type of question downcast the question to its specific type
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
                                $" '{sliderQuestionData.StartValueCaption}', '{sliderQuestionData.EndValueCaption}'";
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
            //update ui
            Questions.Rows.Remove(Questions.Select($"Q_id = {questionId}")[0]);
            Questions.Rows.Add(questionId,
                updatedQuestionData.Text,
                updatedQuestionData.Order,
                //decide the type of the question on whether it was changed or not
                (updatedQuestionType.Equals(originalQuestionType)? originalQuestionType: updatedQuestionType));

        }

        public static void DeleteQuestionFromDB(DataRow[] selectedQuestions)
        {
            using(SqlConnection conn = new SqlConnection(ConnectionString)) { 
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
                //delete question from interface (Questions) in here
                for (int i = 0; i < selectedQuestions.Length; i++)
                {
                    Questions.Rows.Remove(selectedQuestions[i]);
                }
            }
        }


    }
}
