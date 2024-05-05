using Database.models;
using Microsoft.Data.SqlClient;
using System.Data;


namespace Logic
{
    public class QuestionOperations
    {
        // make a general purpose open connection function and send delegates to it
        private QuestionOperations() 
        {
        }

        public static DataTable GetQuestions() 
        {
            return Database.Database.getQuestionsFromDB(); ;
        }

        public static DataRow GetQuestionData(int questionId)
        {
            return Database.Database.getQuestionData(questionId);
        }

        public static DataRow GetQuestionSpecificData(int questionId, string questionType)
        {
            return Database.Database.getQuestionSpecificDataFromDB(questionId, questionType);
        }

        public static void AddQuestion(Question questionData)
        {
          Database.Database.AddQuestionToDB(questionData);
        }

        public static void UpdateQuestion(int questionId, Question updatedQuestionData)
        {
            //recieve the new question general and specific data
           Database.Database.UpdateQuestionOnDB( questionId, updatedQuestionData);
        }

        public static void DeleteQuestion(DataRow[] selectedQuestions)
        {
            Database.Database.DeleteQuestionFromDB(selectedQuestions);
        }

        public static void SetConnectionString()
        {
            //try to obtain the connection string from a file
            //check if the file exists at all
            //get connection string and assign it to the QuestionsOperations property

            //check that file exists
            string filePath = Directory.GetCurrentDirectory() + "\\connectionSettings.txt";
            if (!File.Exists(filePath))
            {
                //create the file if it doesn't exist
                FileStream fs = File.Create(filePath);
                fs.Close();

                //add the default values to the file
                StreamWriter writer = new StreamWriter(filePath);
                writer.WriteLine("Server : HASSANABUGHREEB,\r\nDatabase : Questions_DB,\r\nTrusted_Connection : true,\r\nUser : ,\r\nPassword : ,\r\nEncrypt : false");
                writer.Close();
            }

            //read connection string values from file
            StreamReader sr = new StreamReader(filePath);
            string connectionString = "";

            string[] connectionStringParameters = sr.ReadToEnd().Split(",");
            foreach (string parameter in connectionStringParameters)
            {
                string property = parameter.Split(":")[0].Trim();
                string value = parameter.Split(":")[1].Trim();
                connectionString += $"{property} = {value};\n";
            }

            Database.Database.ConnectionString = connectionString;
        }

    }
}
