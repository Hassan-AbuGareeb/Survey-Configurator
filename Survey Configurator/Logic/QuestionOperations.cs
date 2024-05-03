using Database.models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace Logic
{
    public class QuestionOperations
    {
        // make a general purpose open connection function and send delegates to it


        //private static List<Question> Questions = new List<Question>();
        private static DataTable Questions;
        private static SqlConnection conn;
        private QuestionOperations() { }

        public static DataTable getQuestions() 
        {
            conn = new SqlConnection("Server=HASSANABUGHREEB;Database=Questions_DB;Trusted_Connection=true;Encrypt=false");
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

        public static void AddQuestion(Question questionData)
        {
            //get the Question type from its calss name
            string questionType = questionData.GetType().Name.Split("Q")[0];

            //create db connection
            conn = new SqlConnection("Server=HASSANABUGHREEB;Database=Questions_DB;Trusted_Connection=true;Encrypt=false");

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
            Questions.Rows.Add(questionId, questionData.Text, questionData.Order, questionType);

        }

        public static void UpdateQuestion(Question questionData)
        {

        }

        public static void DeleteQuestion(DataRow[] selectedQuestions)
        {
            conn = new SqlConnection("Server=HASSANABUGHREEB;Database=Questions_DB;Trusted_Connection=true;Encrypt=false");
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
