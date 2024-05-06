using Microsoft.Data.SqlClient;
using System.Data;
using DatabaseLayer.models;
using DatabaseLayer;
using System.Runtime.CompilerServices;

namespace Logic
{
    public class QuestionOperations
    {
        private QuestionOperations() 
        {
        }

        public static DataTable GetQuestions() 
        {
            return Database.getQuestionsFromDB(); ;
        }

        public static DataRow GetQuestionData(int questionId)
        {
            return Database.getQuestionData(questionId);
        }

        public static DataRow GetQuestionSpecificData(int questionId, string questionType)
        {
            return Database.getQuestionSpecificDataFromDB(questionId, questionType);
        }

        public static void AddQuestion(Question questionData)
        {
          Database.AddQuestionToDB(questionData);
        }

        public static void UpdateQuestion(int questionId, Question updatedQuestionData)
        {
            //recieve the new question general and specific data
           Database.UpdateQuestionOnDB( questionId, updatedQuestionData);
        }

        public static void DeleteQuestion(DataRow[] selectedQuestions)
        {
            Database.DeleteQuestionFromDB(selectedQuestions);
        }

        public static string SetConnectionString(string defaultConenctionString)
        {
            //try to obtain the connection string from a file
            //check if the file exists at all
            //get connection string and assign it to the QuestionsOperations property
           
            try {
                string connectionString = "";

                //check that file exists
            string filePath = Directory.GetCurrentDirectory() + "\\connectionSettings.txt";
                if (!File.Exists(filePath))
                {
                    //create the file if it doesn't exist
                    FileStream fs = File.Create(filePath);
                    fs.Close();

                    //add the default values to the file
                    StreamWriter writer = new StreamWriter(filePath);
                    writer.WriteLine(defaultConenctionString);
                    writer.Close();
                }
                else
                { 
                    //read connection string values from file
                    StreamReader sr = new StreamReader(filePath);
                    connectionString = "";

                    string[] connectionStringParameters = sr.ReadToEnd().Split(",");
                    foreach (string parameter in connectionStringParameters)
                    {
                        string property = parameter.Split(':')[0].Trim();
                        string value = parameter.Split(':')[1].Trim();
                        connectionString += $"{property} = {value};\n";
                    }
                    sr.Close();
                 }

                Database.ConnectionString = connectionString;
            }catch(IndexOutOfRangeException ex)
            {
                //log error 

                //return to caller with error
                throw new ArgumentException("Wrong connection parameters",ex);
            }
            catch (Exception)
            {
                //log error

                //either the file can't be created or it is a permission issue
                Database.ConnectionString=defaultConenctionString;
                return "unsucessful";
            }

            return "success";
        }

    }
}
