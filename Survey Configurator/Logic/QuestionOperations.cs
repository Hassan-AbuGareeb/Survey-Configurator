using Microsoft.Data.SqlClient;
using System.Data;
using SharedResources.models;
using DatabaseLayer;
using SharedResources;
using System.Text.Json;

namespace QuestionServices
{
    public class QuestionOperations
    {
        public static bool IsAppRunning = true;
        //changed to true when the user is performing adding, updating or deleting operation
        public static bool OperationOngoing = false;
        //A Datatable collection to hold data temporarly and reduce requests to database
        public static DataTable Questions = new DataTable();
        //a list to temporarily contain the change in the database
        public static List<Question> QuestionsList = new List<Question>();

        private QuestionOperations() 
        {
        }

        #region class main functions
        public static void GetQuestions() 
        {
            try
            {
                Database.getQuestionsFromDB(ref QuestionsList);
            }
            catch (SqlException ex)
            {
                UtilityMethods.LogError(ex);
                throw;
            }
            catch (InvalidOperationException ex)
            {
                UtilityMethods.LogError(ex);
                throw;
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
                throw;
            }
        }

        public static Question GetQuestionData(int pQuestionId)
        {
            Question tQuestionGeneralData = QuestionsList.Find(question => question.Id == pQuestionId);
            return tQuestionGeneralData;
        }

        public static Question GetQuestionSpecificData(int pQuestionId)
        {
            try
            {
                Question tQuestionData = GetQuestionData(pQuestionId);
                return Database.getQuestionSpecificDataFromDB(tQuestionData);
            }
            catch (SqlException ex)
            {
                UtilityMethods.LogError(ex);
                throw;
            }
            catch (InvalidOperationException ex)
            {
                UtilityMethods.LogError(ex);
                throw new InvalidOperationException("problem in connection to database", ex);
            }
            catch (Exception ex)
            {
                UtilityMethods. LogError(ex);
                throw;
            }
        }

        public static void AddQuestion(Question pQuestionData)
        {
            try 
            { 
                //add the question to the database to generate its id and obtain it
                Question tFullQuestionData = Database.AddQuestionToDB(pQuestionData);
                //add question to UI
                //QuestionsList.Add(tFullQuestionData);
            }
            catch (SqlException ex)
            {
                UtilityMethods.LogError(ex);
                throw;
            }
            catch (InvalidOperationException ex)
            {
                UtilityMethods.LogError(ex);
                throw new InvalidOperationException("problem in connection to database", ex);
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
                throw;
            }
        }

        public static void UpdateQuestion(Question pUpdatedQuestionData)
        {
            try
            {
                QuestionType tOriginalQuestionType = GetQuestionData(pUpdatedQuestionData.Id).Type;

                Database.UpdateQuestionOnDB(tOriginalQuestionType, pUpdatedQuestionData);
                //remove from questions list
                //QuestionsList.Remove(QuestionsList.Find(question => question.Id == pUpdatedQuestionData.Id));
                ////add the new Question to the list
                //QuestionsList.Add(pUpdatedQuestionData);
                ////update UI

            }
            catch (SqlException ex)
            {
                UtilityMethods.LogError(ex);
                throw;
            }
            catch (InvalidOperationException ex)
            {
                UtilityMethods.LogError(ex);
                throw new InvalidOperationException("problem in connection to database", ex);
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
                throw;
            }
        }

        public static void DeleteQuestion(Question[] pSelectedQuestions)
        {
            try
            {
                Database.DeleteQuestionFromDB(pSelectedQuestions);
                //delete question from interface (Questions)
                foreach (Question tQuestion in pSelectedQuestions)
                {
                    QuestionsList.Remove(tQuestion);
                }
            }
            catch (SqlException ex)
            {
                UtilityMethods.LogError(ex);
                throw;
            }
            catch (InvalidOperationException ex)
            {
                UtilityMethods.LogError(ex);
                throw new InvalidOperationException("problem in connection to database", ex);
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
                throw;
            }
        }
        #endregion

        #region class utilty functions
        public static bool SetConnectionString()
        {
            //try to obtain the connection string from a file
            try {
                string tConnectionString = "";
                //check that file exists
            string filePath = Directory.GetCurrentDirectory() + "\\connectionSettings.json";
                if (!File.Exists(filePath))
                {
                    //create json file and fill it with default stuff
                    using (FileStream fs = File.Create(filePath)) ;

                    using(StreamWriter writer = new StreamWriter(filePath))
                    {
                        writer.Write(JsonSerializer.Serialize(new ConnectionString()));
                    }
                    //return a value to indicate that a file has been created and to fill it
                }
                else
                { 
                    //read connection string values from file
                    using(StreamReader fileReader = new StreamReader(filePath)) {
                        string tReadConnectionString = fileReader.ReadToEnd();
                        tConnectionString = tReadConnectionString.Trim().Substring(1, tReadConnectionString.Length - 2).Replace(":","=").Replace("\"","").Replace(",",";");
                    }
                 }
                Database.ConnectionString = tConnectionString;
            }
            catch (UnauthorizedAccessException ex)
            {
                UtilityMethods.LogError(ex);
                //handle the unAuthorized access happening
            }
            //this exception should be removed ?
            catch(IndexOutOfRangeException ex)//caused by incorrect connection string which in turn causes the exception by trying to access out of range indexes in the splitted string 
            {
                //log error 
                UtilityMethods.LogError(ex);
                throw new ArgumentException("Wrong connection parameters",ex);
            }
            catch (Exception ex)
            {
                //log error
                UtilityMethods.LogError(ex);
                //either the file can't be created or it is a permission issue
                return false;
            }
            //more exceptions should be added according to the changes made to this function
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
                           QuestionsList.Clear();
                           Database.getQuestionsFromDB(ref QuestionsList);
                           //update UI somehow
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                UtilityMethods.LogError(ex);
                throw;
            }
            catch (InvalidOperationException ex)
            {
                UtilityMethods.LogError(ex);
                throw;
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
                throw;
            }
        }

        #endregion
    }
}
