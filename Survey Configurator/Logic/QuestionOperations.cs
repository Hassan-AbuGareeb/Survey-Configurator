using Microsoft.Data.SqlClient;
using System.Data;
using DatabaseLayer.models;
using DatabaseLayer;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Reflection.PortableExecutable;

namespace Logic
{
    public class QuestionOperations
    {
        public static bool IsAppRunning = true;
        public static DataTable Questions = new DataTable();

        private QuestionOperations() 
        {
        }

        public static void GetQuestions() 
        {
            Questions =  Database.getQuestionsFromDB();
        }

        public static DataRow GetQuestionData(int questionId)
        {
            DataRow questionGeneralData = Questions.Select($"Q_id = {questionId}")[0];
            return questionGeneralData;
        }

        public static DataRow GetQuestionSpecificData(int questionId, string questionType)
        {
            return Database.getQuestionSpecificDataFromDB(questionId, questionType);
        }

        public static void AddQuestion(Question questionData)
        {
            int questionId = Database.AddQuestionToDB(questionData);
            string questionType = questionData.GetType().Name.Split("Q")[0];
            Questions.Rows.Add(questionId, questionData.Text, questionData.Order, questionType);
           
        }

        public static void UpdateQuestion(int questionId, Question updatedQuestionData)
        {
            string originalQuestionType = GetQuestionData(questionId)["Q_type"].ToString();
            string updatedQuestionType = updatedQuestionData.GetType().Name.Split("Q")[0];
            Database.UpdateQuestionOnDB( questionId, originalQuestionType, updatedQuestionData);
            Questions.Rows.Remove(Questions.Select($"Q_id = {questionId}")[0]);
            Questions.Rows.Add(questionId,
            updatedQuestionData.Text,
            updatedQuestionData.Order,
            //decide the type of the question on whether it was changed or not
            (updatedQuestionType.Equals(originalQuestionType) ? originalQuestionType : updatedQuestionType));
        }

        public static void DeleteQuestion(DataRow[] selectedQuestions)
        {
            Database.DeleteQuestionFromDB(selectedQuestions);
            //delete question from interface (Questions) in here
            foreach (DataRow question in selectedQuestions)
            {
                Questions.Rows.Remove(question);
            }
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
            while (IsAppRunning)
            {
                await Task.Delay(10000);
                //get checksum again to detect change
                long newChecksum = Database.getChecksum();
                if (currentChecksum != newChecksum)
                {
                    //data changed
                    currentChecksum = newChecksum;
                    DataTable updatedQuestions = Database.getQuestionsFromDB();
                    Questions.Clear();
                    for (int i = 0; i < updatedQuestions.Rows.Count; i++)
                    {
                        DataRow currentQuestion = updatedQuestions.Rows[i];
                        Questions.Rows.Add(currentQuestion["Q_id"], currentQuestion["Q_text"], currentQuestion["Q_order"], currentQuestion["Q_type"]);
                    }
                }
            }
        }

    }
}
