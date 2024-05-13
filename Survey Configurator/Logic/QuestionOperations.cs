using Microsoft.Data.SqlClient;
using System.Data;
using QuestionDB.models;
using DatabaseLayer;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Reflection.PortableExecutable;

namespace QuestionServices
{
    public class QuestionOperations
    {
        public static bool IsAppRunning = true;
        //changed to true when the user is performing adding, updating or deleting operation
        public static bool OperationOngoing = false;
        //A Datatable collection to hold data temporarly and reduce requests to database
        public static DataTable Questions = new DataTable();

        private QuestionOperations() 
        {
        }

        #region class main functions
        public static void GetQuestions() 
        {
            try
            {
                Questions = Database.getQuestionsFromDB();
            }
            catch (SqlException ex)
            {
                LogError(ex);
                throw;
            }
            catch (InvalidOperationException ex)
            {
                LogError(ex);
                throw;
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw;
            }
        }

        public static DataRow GetQuestionData(int pQuestionId)
        {
            DataRow tQuestionGeneralData = Questions.Select($"Q_id = {pQuestionId}")[0];
            return tQuestionGeneralData;
        }

        public static DataRow GetQuestionSpecificData(int pQuestionId, string pQuestionType)
        {
            try
            {
                return Database.getQuestionSpecificDataFromDB(pQuestionId, pQuestionType);
            }
            catch (SqlException ex)
            {
                LogError(ex);
                throw;
            }
            catch (InvalidOperationException ex)
            {
                LogError(ex);
                throw new InvalidOperationException("problem in connection to database", ex);
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw;
            }
        }

        public static void AddQuestion(Question pQuestionData)
        {
            try 
            { 
                //add the question to the database to generate its id and obtain it
                int tQuestionId = Database.AddQuestionToDB(pQuestionData);
                string tQuestionType = pQuestionData.GetType().Name.Split("Q")[0];
                //add question to UI
                Questions.Rows.Add(tQuestionId, pQuestionData.Text, pQuestionData.Order, tQuestionType);
            }
            catch (SqlException ex)
            {
                LogError(ex);
                throw;
            }
            catch (InvalidOperationException ex)
            {
                LogError(ex);
                throw new InvalidOperationException("problem in connection to database", ex);
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw;
            }
        }

        public static void UpdateQuestion(int pQuestionId, Question pUpdatedQuestionData)
        {
            try 
            { 
                string tOriginalQuestionType = GetQuestionData(pQuestionId)["Q_type"].ToString();
                string tUpdatedQuestionType = pUpdatedQuestionData.GetType().Name.Split("Q")[0];

                Database.UpdateQuestionOnDB(pQuestionId, tOriginalQuestionType, pUpdatedQuestionData);

                //update UI
                Questions.Rows.Remove(Questions.Select($"Q_id = {pQuestionId}")[0]);
                Questions.Rows.Add(pQuestionId,
                pUpdatedQuestionData.Text,
                pUpdatedQuestionData.Order,
                //decide the type of the question on whether it was changed or not
                (tUpdatedQuestionType.Equals(tOriginalQuestionType) ? tOriginalQuestionType : tUpdatedQuestionType));
            }
            catch (SqlException ex)
            {
                LogError(ex);
                throw;
            }
            catch (InvalidOperationException ex)
            {
                LogError(ex);
                throw new InvalidOperationException("problem in connection to database", ex);
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw;
            }
        }

        public static void DeleteQuestion(DataRow[] pSelectedQuestions)
        {
            try
            {
                Database.DeleteQuestionFromDB(pSelectedQuestions);
                //delete question from interface (Questions)
                foreach (DataRow tQuestion in pSelectedQuestions)
                {
                    Questions.Rows.Remove(tQuestion);
                }
            }
            catch (SqlException ex)
            {
                LogError(ex);
                throw;
            }
            catch (InvalidOperationException ex)
            {
                LogError(ex);
                throw new InvalidOperationException("problem in connection to database", ex);
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw;
            }
        }
        #endregion

        #region class utilty functions
        public static bool SetConnectionString()
        {
            //try to obtain the connection string from a file
            try {
                string connectionString = "";

                //check that file exists
            string filePath = Directory.GetCurrentDirectory() + "\\connectionSettings.txt";
                if (!File.Exists(filePath))
                {
                    //create json file and fill it with default stuff
                    using (FileStream fs = File.Create(filePath));
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
                LogError(ex);
                throw new ArgumentException("Wrong connection parameters",ex);
            }
            catch (Exception ex)
            {
                //log error
                LogError(ex);
                //either the file can't be created or it is a permission issue
                return false;
            }
            return true;
        }

        public static async void CheckDataBaseChange()
        {
            try
            {
                //get checksum of the database current version of data
                long currentChecksum = Database.getChecksum();
                while (IsAppRunning)
                {
                    await Task.Delay(10000);
                    if (currentChecksum==0)
                        continue;
                    if(!OperationOngoing) 
                    {
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
            catch (SqlException ex)
            {
                LogError(ex);
                throw;
            }
            catch (InvalidOperationException ex)
            {
                LogError(ex);
                throw;
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw;
            }
        }

        public static void LogError(Exception pExceptionData)
        {
            try
            {
                //collect the error info to log to the file
                string[] tExceptionDetails = [
                    $"{DateTime.Now.ToUniversalTime()} UTC",
                    $"Exception: {pExceptionData.GetType().Name}",
                    $"Exception message: {pExceptionData.Message}",
                    $"Stack trace:\n{pExceptionData.StackTrace}"];
                //check that file exists
                string tDirectoryPath = Directory.GetCurrentDirectory() + "\\errorlogs";
                if (!Directory.Exists(tDirectoryPath))
                {
                    Directory.CreateDirectory(tDirectoryPath);
                }

                string tFilePath = tDirectoryPath + "\\errorlog.txt";
                if (!File.Exists(tFilePath))
                {
                    //create the file if it doesn't exist
                    FileStream fs = File.Create(tFilePath);
                    fs.Close();
                }

                //add the default values to the file
                StreamWriter tWriter = File.AppendText(tFilePath);
                tWriter.WriteLine(string.Join(",\n", tExceptionDetails) + "\n\n--------\n");
                tWriter.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while logging: {ex.Message}");
            }
        }
        #endregion
    }
}
