using DatabaseLayer;
using SharedResources;
using SharedResources.models;
using System.Text.Json;

namespace QuestionServices
{
    public class QuestionOperations
    {
        public static event EventHandler<string> DataBaseChangedEvent;

        public static event EventHandler DataBaseNotConnectedEvent;

        private const int cDatabaseReconnectMaxAttempts = 3;

        //changed to true when the user is performing adding, updating or deleting operation
        public static bool OperationOngoing = false;
        //a list to temporarily contain the change in the database
        public static List<Question> QuestionsList = new List<Question>();

        private QuestionOperations() 
        {
        }

        #region class main functions
        public static OperationResult GetQuestions() 
        {
            try
            {
                return Database.getQuestionsFromDB(ref QuestionsList);
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
                return new OperationResult(ErrorTypes.UnknownError, "An Unknown error occured");
            }
        }

        public static Question GetQuestionData(int pQuestionId)
        {
            try 
            { 
            Question tQuestionGeneralData = QuestionsList.Find(question => question.Id == pQuestionId);
            return tQuestionGeneralData;
            }
            catch(Exception ex) 
            {
                UtilityMethods.LogError(ex);
                return null;
            }
        }

        public static OperationResult GetQuestionSpecificData(int pQuestionId, ref Question pQuestionSpecificData)
        {
            try
            {
                Question tQuestionData = GetQuestionData(pQuestionId);
                OperationResult tQuestionSpecificDataResult = Database.getQuestionSpecificDataFromDB(tQuestionData, ref pQuestionSpecificData);
                if(tQuestionSpecificDataResult.IsSuccess && pQuestionSpecificData == null)
                {
                    return new OperationResult(ErrorTypes.NullValueError, "couldn't get the requested question data");
                }
                else
                {
                    return tQuestionSpecificDataResult;
                }
            }
            catch (Exception ex)
            {
                UtilityMethods. LogError(ex);
                return new OperationResult(ErrorTypes.UnknownError, "An Unknown error occured");
            }
        }

        public static OperationResult AddQuestion(Question pQuestionData)
        {
            try 
            { 
                //add the question to the database to generate its id and obtain it
                OperationResult tAddQuestionResult = Database.AddQuestionToDB(pQuestionData);
                //on successful question addition to Database add it to the Questions List
                if (tAddQuestionResult.IsSuccess)
                {
                    QuestionsList.Add(pQuestionData);
                    //notify UI of change
                    DataBaseChangedEvent?.Invoke(typeof(QuestionOperations), "Added a new question");
                }
               
                return tAddQuestionResult;
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
                return new OperationResult(ErrorTypes.UnknownError, "An Unknown error occured.");
            }
        }

        public static OperationResult UpdateQuestion(Question pUpdatedQuestionData)
        {
            try
            {
                QuestionType tOriginalQuestionType = GetQuestionData(pUpdatedQuestionData.Id).Type;

                OperationResult tQuestionUpdatedResult = Database.UpdateQuestionOnDB(tOriginalQuestionType, pUpdatedQuestionData);
                if (tQuestionUpdatedResult.IsSuccess)
                {
                    //remove from questions list
                    QuestionsList.Remove(QuestionsList.Find(question => question.Id == pUpdatedQuestionData.Id));
                    //add the new Question to the list
                    QuestionsList.Add(pUpdatedQuestionData);
                    //notify UI of change
                    DataBaseChangedEvent?.Invoke(typeof(QuestionOperations), "Updated question data");
                }
                return tQuestionUpdatedResult;
                
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
                return new OperationResult(ErrorTypes.UnknownError, "an unknown error occured");
            }
        }

        public static OperationResult DeleteQuestion(List<Question> pSelectedQuestions)
        {
            try
            {
                OperationResult tDeleteQuestionsResult =  Database.DeleteQuestionFromDB(pSelectedQuestions);

                if (!tDeleteQuestionsResult.IsSuccess)
                {
                    return tDeleteQuestionsResult;
                }
                //delete question from List (Questions)
                foreach (Question tQuestion in pSelectedQuestions)
                {
                    QuestionsList.Remove(tQuestion);
                }
                //notify UI of change
                DataBaseChangedEvent?.Invoke(typeof(QuestionOperations), "Deleted question");
                return new OperationResult();
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
                return new OperationResult(ErrorTypes.UnknownError, "An Unkown error occured");
            }
        }
        #endregion

        #region class utilty functions
        public static OperationResult TestDBConnection()
        {
            try 
            { 
                return Database.TestDataBaseConnection();
            }
            catch(Exception ex)
            {
                UtilityMethods.LogError(ex);
                return new OperationResult(ErrorTypes.UnknownError, "Unkown error occured");
            }

        }

        public static OperationResult SetConnectionString()
        {
            //try to obtain the connection string from a file
            try {
                string tConnectionString = "";
                //check that file exists
                string tFilePath = Directory.GetCurrentDirectory() + "\\connectionSettings.json";
                if (!File.Exists(tFilePath))
                {
                    //create json file and fill it with default stuff
                    using (FileStream tFs = File.Create(tFilePath)) ;

                    using(StreamWriter tWriter = new StreamWriter(tFilePath))
                    {
                        tWriter.Write(JsonSerializer.Serialize(new ConnectionString()));
                    }
                    //return a value to indicate that a file has been created and to fill it
                }
                else
                { 
                    //read connection string values from tFilePath
                    using(StreamReader tFileReader = new StreamReader(tFilePath)) {
                        string tReadConnectionString = tFileReader.ReadToEnd();
                        tConnectionString = tReadConnectionString.Trim().Substring(1, tReadConnectionString.Length - 2).Replace(":","=").Replace("\"","").Replace(",",";");
                    }
                 }

                Database.ConnectionString = tConnectionString;
                return new OperationResult();
            }
            catch (UnauthorizedAccessException ex)
            {
                UtilityMethods.LogError(ex);
                //handle the unAuthorized access happening
                return new OperationResult(ErrorTypes.UnAuthorizedAccessException, "You have restrictions on file operations please refer to your system admin");
            }
            catch (Exception ex)
            {
                //log error
                UtilityMethods.LogError(ex);
                //either the file can't be created or it is a permission issue
                return new OperationResult(ErrorTypes.UnknownError, "An Unknown error occured.");
            }
        }

        public static OperationResult StartCheckingDataBaseChange()
        {
            try 
            {
                Thread tCheckThread = new Thread(()=>CheckDataBaseChange(Thread.CurrentThread));
                tCheckThread.IsBackground = true;
                tCheckThread.Start();
                return new OperationResult();
            }
            catch(Exception ex)
            {
                UtilityMethods.LogError(ex);
                return new OperationResult(ErrorTypes.UnknownError, "Unkown Error occured");
            }
        }

        public static void CheckDataBaseChange(Thread pMainThread)
        {
            try
            {
                //get checksum of the database current version of data
                long tcurrentChecksum=0;
                int tDatabaseConnectionRetryCount = 0;
                Database.getChecksum(ref tcurrentChecksum);
                while (pMainThread.IsAlive)
                {
                    Thread.Sleep(10000);
                    if (tcurrentChecksum == 0)
                        continue;
                    if(!OperationOngoing) 
                    {
                        //get checksum again to detect change
                        long tNewChecksum = 0;
                        OperationResult tNewChecksumResult = Database.getChecksum(ref tNewChecksum);
                        if (tNewChecksumResult.IsSuccess) {
                            if (tcurrentChecksum != tNewChecksum)
                            {
                                //data changed
                               tcurrentChecksum = tNewChecksum;
                           
                               QuestionsList.Clear();
                               Database.getQuestionsFromDB(ref QuestionsList);
                               //notify UI of database change
                               DataBaseChangedEvent?.Invoke(typeof(QuestionOperations), "Database externally changed");
                            }
                        }
                        else
                        {
                            tDatabaseConnectionRetryCount++;
                            if (tDatabaseConnectionRetryCount > cDatabaseReconnectMaxAttempts)
                            {
                                DataBaseNotConnectedEvent?.Invoke(typeof(QuestionOperations), EventArgs.Empty);
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
            }
        }

        #endregion
    }
}
