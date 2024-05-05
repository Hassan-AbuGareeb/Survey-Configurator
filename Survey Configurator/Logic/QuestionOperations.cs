using Database.models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace Logic
{
    public class QuestionOperations
    {
        // make a general purpose open connection function and send delegates to it


        private static DataTable Questions;
        private static SqlConnection conn;
        public static string ConnectionString="" ;
        private QuestionOperations() 
        {
        
        }

        public static DataTable getQuestions() 
        {
            conn = new SqlConnection(ConnectionString);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM Question";
            conn.Open();
            Console.WriteLine("connected");
            DbDataReader reader = cmd.ExecuteReader();
            Questions = new DataTable();
            Questions.Load(reader, LoadOption.Upsert);
            conn.Close();
            return Questions;
        }

        public static DataRow getQuestionData(int questionId)
        {
            DataRow questionGeneralData = Questions.Select($"Q_id = {questionId}")[0];
            return questionGeneralData;
        }

        public static DataRow getQuestionSpecificData(int questionId, string questionType)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);

            SqlCommand getQuestionDataCmd = new SqlCommand();
            getQuestionDataCmd.CommandType = CommandType.Text;
            getQuestionDataCmd.CommandText = $"SELECT * FROM {questionType} WHERE Q_id = {questionId}";
            getQuestionDataCmd.Connection = conn;
            conn.Open();
            SqlDataReader reader = getQuestionDataCmd.ExecuteReader();
            DataTable tempTable = new DataTable();
            tempTable.Load(reader);
            reader.Close();
            return tempTable.Rows[0];
        }

        public static void AddQuestion(Question questionData)
        {
            //get the Question type from its calss name
            string questionType = questionData.GetType().Name.Split("Q")[0];
            //escaping any single quote as not to cause any propblems in sql statements
            questionData.Text = questionData.Text.Replace("'", "''");
            //create db connection
            conn = new SqlConnection(ConnectionString);

            //insert question data in the question table
            SqlCommand insertQuestionCmd = conn.CreateCommand();
            insertQuestionCmd.CommandType = CommandType.Text;
            insertQuestionCmd.CommandText = $"INSERT INTO Question (Q_text, Q_order, Q_type) OUTPUT INSERTED.Q_id VALUES ('{questionData.Text}', {questionData.Order}, '{questionType}')";
            
            conn.Open();
            //insert the row data to the question and return the id of the created question
            int questionId =(int)insertQuestionCmd.ExecuteScalar();

            SqlCommand insertQuestionTypeCmd = conn.CreateCommand();
            ////get the specific values for the question type
            string questionTypeSpecificAttributes = "";
            string questionTypeSpecificValues = "";
            ////for each type of question downcast the question to its specific type

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
                    questionTypeSpecificValues+= $"{sliderQuestionData.StartValue}, {sliderQuestionData.EndValue}," +
                        $" '{sliderQuestionData.StartValueCaption}', '{sliderQuestionData.EndValueCaption}'";
                    break;
                case "Stars":
                    StarsQuestion starsQuestionData = (StarsQuestion)questionData;
                    questionTypeSpecificAttributes+= "Num_of_stars";
                    questionTypeSpecificValues += $"{starsQuestionData.NumberOfStars}";
                    break;
            }
            insertQuestionTypeCmd.CommandType = CommandType.Text;
            insertQuestionTypeCmd.CommandText = $"INSERT INTO {questionType} (Q_id, {questionTypeSpecificAttributes}) VALUES ({questionId}, {questionTypeSpecificValues})";
            insertQuestionTypeCmd.ExecuteNonQuery();
            conn.Close();

            //add the question to the UI
            Questions.Rows.Add(questionId, questionData.Text.Replace("''","'"), questionData.Order, questionType);

        }

        public static void UpdateQuestion(int questionId, Question updatedQuestionData)
        {
            //recieve the new question general and specific data
            string originalQuestionType = Questions.Select($"Q_id = {questionId}")[0]["Q_type"].ToString();
            updatedQuestionData.Text = updatedQuestionData.Text.Replace("'", "''");

            conn = new SqlConnection(ConnectionString);

            if (updatedQuestionData.GetType().Name.Split("Q")[0].Equals(originalQuestionType))
            {//type of question wasn't  changed
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
                updateQuestionDataCmd.CommandText = $"UPDATE Question SET Q_order = {updatedQuestionData.Order}, Q_text = '{updatedQuestionData.Text}' WHERE Q_id = {questionId}";
                updateQuestionDataCmd.Connection = conn;
                conn.Open();
                updateQuestionSpecificDataCmd.ExecuteNonQuery();
                updateQuestionDataCmd.ExecuteNonQuery();
                conn.Close();

                //update ui
                Questions.Rows.Remove(Questions.Select($"Q_id = {questionId}")[0]);
                Questions.Rows.Add(questionId, updatedQuestionData.Text, updatedQuestionData.Order, originalQuestionType);
            }
            else
            {//type of question changed
                //delete the questions specific old data first
                SqlCommand deleteSpecificQuestionDataCmd = new SqlCommand($"DELETE FROM {originalQuestionType} WHERE Q_id = {questionId}",conn);
                //update the general question data
                SqlCommand updateQuestionDataCmd = new SqlCommand
                    ($"UPDATE Question SET Q_order = {updatedQuestionData.Order}, Q_text = '{updatedQuestionData.Text}'," +
                    $" Q_type = '{updatedQuestionData.GetType().Name.Split("Q")[0]}' WHERE Q_id = {questionId}",
                    conn);
                //create a new row in the specific question type table
                SqlCommand insertQuestionTypeCmd = conn.CreateCommand();
                ////get the specific values for the question type
                string questionTypeSpecificAttributes = "";
                string questionTypeSpecificValues = "";
                ////for each type of question downcast the question to its specific type
                string updatedQuestionType = updatedQuestionData.GetType().Name.Split("Q")[0];
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
                
                conn.Open();
                //exectue commands on database
                deleteSpecificQuestionDataCmd.ExecuteNonQuery();
                updateQuestionDataCmd.ExecuteNonQuery();
                insertQuestionTypeCmd.ExecuteNonQuery();
                conn.Close();
                //udpate ui
                Questions.Rows.Remove(Questions.Select($"Q_id = {questionId}")[0]);
                Questions.Rows.Add(questionId, updatedQuestionData.Text.Replace("''", "'"), updatedQuestionData.Order, updatedQuestionType);
            }

        }

        public static void DeleteQuestion(DataRow[] selectedQuestions)
        {
            conn = new SqlConnection(ConnectionString);
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
            conn.Close();
        }
    }
}
