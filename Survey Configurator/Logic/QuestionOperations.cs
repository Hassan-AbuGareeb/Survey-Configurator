﻿using DatabaseLayer;
using SharedResources;
using SharedResources.models;
using System.Text.Json;

namespace QuestionServices
{
    public class QuestionOperations
    {
        /// <summary>
        /// this class is the one responsible for the communication between the UI layer
        /// and the database, it gets the requested data from the database and applies 
        /// any kind of logic to it before sending it to the UI layer.
        /// it's also responsible for doing other operations such as mointoring the database
        /// for any change and informing the UI layer when it occurs, and more operations mentioned below.
        /// </summary>



        //event handler for any change that happens to the database from any source
        public static event EventHandler<string> DataBaseChangedEvent;
        //event handler for when the database stops responding
        public static event EventHandler DataBaseNotConnectedEvent;

        private const int cDatabaseReconnectMaxAttempts = 3;

        //changed to true when the user is performing adding, updating or deleting operation
        public static bool OperationOngoing = false;
        //a list to temporarily contain the questions data fetched from the database, 
        //and acts as a data source for the UI to faciltate data transfer and fetching.
        public static List<Question> QuestionsList = new List<Question>();

        private QuestionOperations() 
        {
        }

        #region class main functions
        /// <summary>
        /// this function is responsible for fetching the General Question info (Id, text, order, type)
        /// from the database, it fills the QuestionList field with the obtained questions data.
        /// </summary>
        /// <returns>OperationResult to indicate whether the info got fetched from the database or not</returns>
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

        /// <summary>
        /// responsible for fetching the question general data
        /// </summary>
        /// <param name="pQuestionId">the Id of the quesiton whose information must be fetched</param>
        /// <returns>OperationResult to indicate whether the info got fetched from the database or not</returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pQuestionId">the Id of the quesiton whose information must be fetched</param>
        /// <param name="pQuestionSpecificData">a reference question object to fill with the full question info</param>
        /// <returns>OperationResult to indicate whether the info got fetched from the database or not</returns>
        public static OperationResult GetQuestionSpecificData(int pQuestionId, ref Question pQuestionSpecificData)
        {
            try
            {
                //get the general question data to add to it its specific data
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

        /// <summary>
        /// adds a new question object to the database, and the QuestionsList collection
        /// and notify the UI layer to update its data, note that the question won't
        /// be added to the QuesitonsList nor will the UI be notified if it isn't
        /// added to the database
        /// </summary>
        /// <param name="pQuestionData">full data of the question</param>
        /// <returns>OperationResult to indicate whether the question got added to the database or not</returns>
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

        /// <summary>
        /// edit the question data, by getting its original type from the QuestionsList 
        /// collection and sending it along with the updated data to determine whether
        /// its type was changed or not, and as the previous one, the UI won't be 
        /// changed unless the database operation was successful.
        /// </summary>
        /// <param name="pUpdatedQuestionData">updated question data</param>
        /// <returns>OperationResult to indicate whether the question data go edited or not</returns>
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

        /// <summary>
        /// delete the selected question/questions from db, notify UI of deletion
        /// only if it was successfull.
        /// </summary>
        /// <param name="pSelectedQuestions"></param>
        /// <returns>OperationResult to indicate whether the question data go deleted or not</returns>
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
        /// <summary>
        /// this function tests whether a connection can be established to the database
        /// </summary>
        /// <returns>OperationResult object to indicate the success or failure of the connection to database</returns>
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

        /// <summary>
        /// this function obtains the connection string from the connectionString.json file
        /// if it exists, otherwise create the file if it doesn't exist and fill it with the 
        /// default values for the connection string properties.
        /// </summary>
        /// <returns>OperationResult object to indicate the success or failure of the connection to database</returns>
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
                        //re-format the connection string obtained from the file in the correct connection string format
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

        /// <summary>
        /// this function create a thread and starts a funciton on it to start monitoring the database
        /// the thread runs in the background as to not block the main thread it the function on the 
        /// thread, this function only ever returns a result in case the thread won't start at all
        /// or an error occured within the funciton called on the thread.
        /// </summary>
        /// <returns>OperationResult object to indicate whether the database monitoring is successful or failed</returns>
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

        /// <summary>
        /// this function keeps checking the database every 10 seconds
        /// for any change, it uses the database checksum as the base
        /// for comparison, as it changes and produce a unique value 
        /// on every change, also the database won't be checked if 
        /// there's any operation going on (add, edit, delete) as to 
        /// avoid any errors or conflits in changing data.
        /// in the case of the database refusing to connect multiple times
        /// this function raises an event and returns a failed operationResult object.
        /// </summary>
        /// <param name="pMainThread"></param>
        public static void CheckDataBaseChange(Thread pMainThread)
        {
            try
            {
                int tDatabaseConnectionRetryCount = 0;
                //get checksum of the database current version of data
                long tcurrentChecksum =0;
                Database.getChecksum(ref tcurrentChecksum);
                //keep the function running while main thread is running
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

                                //reset the connection retry counter on successful data change
                                tDatabaseConnectionRetryCount = 0;
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
