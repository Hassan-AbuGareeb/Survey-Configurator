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
        //A Datatable collection to hold data temporarly and reduce requests to database
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
            //add the question to the database to generate its id and obtain it
            int questionId = Database.AddQuestionToDB(questionData);
            string questionType = questionData.GetType().Name.Split("Q")[0];
            //add question to UI
            Questions.Rows.Add(questionId, questionData.Text, questionData.Order, questionType);
           
        }

        public static void UpdateQuestion(int questionId, Question updatedQuestionData)
        {
            string originalQuestionType = GetQuestionData(questionId)["Q_type"].ToString();
            string updatedQuestionType = updatedQuestionData.GetType().Name.Split("Q")[0];

            Database.UpdateQuestionOnDB( questionId, originalQuestionType, updatedQuestionData);

            //update UI
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
            //delete question from interface (Questions)
            foreach (DataRow question in selectedQuestions)
            {
                Questions.Rows.Remove(question);
            }
        }

        public static bool SetConnectionString(string defaultConenctionString)
        {
            //try to obtain the connection string from a file
            try {
                string connectionString = "";

                //check that file exists
            string filePath = Directory.GetCurrentDirectory() + "\\connectionSettings.txt";
                if (!File.Exists(filePath))
                {
                    using (FileStream fs = File.Create(filePath));

                    //add the default values to the file
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        writer.WriteLine(defaultConenctionString);
                    }
                }
                else
                { 
                    //read connection string values from file
                    using(StreamReader fileReader = new StreamReader(filePath)) {

                    string[] connectionStringParameters = fileReader.ReadToEnd().Split(",");
                        foreach (string parameter in connectionStringParameters)
                        {
                            string property = parameter.Split(':')[0].Trim();
                            string value = parameter.Split(':')[1].Trim();
                            connectionString += $"{property} = {value};\n";
                        }
                    }
                 }
                Database.ConnectionString = connectionString;
            }
            catch(IndexOutOfRangeException ex)//caused by incorrect connection string which in turn causes the exception by trying to access out of range indexes in the splitted string 
            {
                //log error 
                Database.LogError(ex);
                throw new ArgumentException("Wrong connection parameters",ex);
            }
            catch (Exception ex)
            {
                //log error
                Database.LogError(ex);
                //either the file can't be created or it is a permission issue
                Database.ConnectionString=defaultConenctionString;
                return false;
            }
            return true;
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
                    //fill Questions collection with updated data from db
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
