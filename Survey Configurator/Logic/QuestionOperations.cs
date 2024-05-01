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
            cmd.CommandText = "SELECT Q_order, Q_text, Q_type  FROM Question";
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
            string questionType = questionData.GetType().Name.Split("Q")[0];
            //create db connection
            conn = new SqlConnection("Server=HASSANABUGHREEB;Database=Questions_DB;Trusted_Connection=true;Encrypt=false");
            //insert a row in the question table
            SqlCommand insertQuestionCmd = conn.CreateCommand();
            insertQuestionCmd.CommandType = CommandType.Text;
            insertQuestionCmd.CommandText = $"INSERT INTO Question (Q_text, Q_order, Q_type) VALUES ('{questionData.Text}', {questionData.Order}, '{questionType}')";
            
            //add the question and get its id
           
            conn.Open();
            insertQuestionCmd.ExecuteNonQuery();
            //get the newly created row


            SqlCommand getAddedQuestionCmd = conn.CreateCommand();
            getAddedQuestionCmd.CommandType = CommandType.Text;
            getAddedQuestionCmd.CommandText = $"SELECT * from Question WHERE Q_text = '{questionData.Text}' AND Q_order = {questionData.Order}";


            ////get the question id after adding the question to the database
            DbDataReader AddedQuestionDataReader = getAddedQuestionCmd.ExecuteReader();
            AddedQuestionDataReader.Read();
            int questionId = (int)AddedQuestionDataReader["Q_id"];
            AddedQuestionDataReader.Close();


            SqlCommand insertQuestionTypeCmd = conn.CreateCommand();
            ////get the specific values for the question type
            string questionTypeSpecificAttributes = "";
            string questionTypeSpecificValues = "";
            ////for each type of question downcast the question to its specific type

            switch (questionType)
            {
                case "Smiley":
                    SmileyQuestion smileyQuestionData = (SmileyQuestion)questionData;
                    questionTypeSpecificAttributes.Concat("Num_of_faces");
                    questionTypeSpecificValues.Concat($"{smileyQuestionData.NumberOfSmileyFaces}");
                    break;
                case "Slider":
                    SliderQuestion sliderQuestionData = (SliderQuestion)questionData;
                    questionTypeSpecificAttributes.Concat("Start_value, End_value, Start_value_caption, End_value_caption");
                    questionTypeSpecificValues.Concat($"{sliderQuestionData.StartValue}, {sliderQuestionData.EndValue}," +
                        $" '{sliderQuestionData.StartValueCaption}', '{sliderQuestionData.EndValueCaption}'");
                    break;
                case "Stars":
                    StarsQuestion starsQuestionData = (StarsQuestion)questionData;
                    questionTypeSpecificAttributes.Concat("Num_of_stars");
                    questionTypeSpecificValues.Concat($"{starsQuestionData.NumberOfStars}");
                    break;
            }
            insertQuestionTypeCmd.CommandType = CommandType.Text;
            insertQuestionTypeCmd.CommandText = $"INSERT INTO {questionType} (Q_id, {questionTypeSpecificAttributes}) VALUES ({questionId}, {questionTypeSpecificValues})";
            insertQuestionTypeCmd.ExecuteNonQuery();

            conn.Close();
            //if the insert operation is successful add the question to the UI


        }
    }
}
