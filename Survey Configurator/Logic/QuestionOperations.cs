using Microsoft.Data.SqlClient;
using System.Data;
using DatabaseLayer.models;
using DatabaseLayer;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace Logic
{
    public class QuestionOperations
    {
        public static bool IsAppRunning = true;

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
            }
            catch(IndexOutOfRangeException ex)//caused by incorrect connection string which in turn causes the exception by trying to access out of range indexes in the splitted string 
            {
                //log error 
                Database.LogError(ex);
                //return to caller with error
                throw new ArgumentException("Wrong connection parameters",ex);
            }
            catch (Exception ex)
            {
                //log error
                Database.LogError(ex);
                //either the file can't be created or it is a permission issue
                Database.ConnectionString=defaultConenctionString;
                return "unsucessful";
            }

            return "success";
        }

        public static async void CheckDataBaseChange()
        {
            //get checksum of the database current version of data
            long currentChecksum = Database.getChecksum();
            while(IsAppRunning)
            {
                await Task.Delay(10000);
                //get checksum again to detect change
                long newChecksum = Database.getChecksum();
                if(currentChecksum != newChecksum)
                {
                    //data changed
                    Database.getQuestionsFromDB();
                    currentChecksum = newChecksum;
                }
            }
        }

    }
}
