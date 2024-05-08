using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using DatabaseLayer.models;
using Microsoft.Data.SqlClient;
namespace DatabaseLayer
{
    public class Database
    {
        public static string ConnectionString;

        private Database() { }

        public static DataTable getQuestionsFromDB()
        {
            try { 
                using (SqlConnection conn = new SqlConnection(ConnectionString)) 
                {
                    DbDataReader reader;
                    conn.Open();
                    SqlCommand getQuestionsDataCmd = new SqlCommand("SELECT * FROM Question",conn);
                    reader = getQuestionsDataCmd.ExecuteReader();
                    DataTable QuestionsData = new DataTable();
                    QuestionsData.Load(reader);
                    return QuestionsData;
                }
            }catch (SqlException ex)
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

        public static DataRow getQuestionSpecificDataFromDB(int questionId, string questionType)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString)) 
                {
                    DbDataReader reader;
                    conn.Open();
                    SqlCommand getQuestionSpecificData = new SqlCommand($"SELECT * FROM {questionType} WHERE Q_id = {questionId}", conn);
                    reader = getQuestionSpecificData.ExecuteReader();
                    DataTable tempTable = new DataTable();
                    tempTable.Load(reader);
                    reader.Close();
                    return tempTable.Rows[0];
                }
            }catch (SqlException ex)
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

        public static int AddQuestionToDB(Question questionData)
        {
            try
            {
                //get the Question type from its calss name
                string questionType = questionData.GetType().Name.Split("Q")[0];
                //create db connection
                using(SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    //insert question data in the question table
                    SqlCommand insertQuestionCmd = new SqlCommand($"INSERT INTO Question (Q_text, Q_order, Q_type) OUTPUT INSERTED.Q_id VALUES (@Q_text, {questionData.Order}, '{questionType}')",
                        conn);
                    insertQuestionCmd.Parameters.Add(new SqlParameter("@Q_text", questionData.Text));

                    //insert the row data to the question and return the id of the created question
                    int questionId = (int)insertQuestionCmd.ExecuteScalar();

                    SqlCommand insertQuestionTypeCmd = conn.CreateCommand();
                    //get the specific values for the question type
                    string questionTypeSpecificAttributes = "";
                    string questionTypeSpecificValues = "";
                    //for each type of question downcast the question to its specific type
                    switch (questionType)
                    {
                        case "Smiley":
                            SmileyQuestion smileyQuestionData = (SmileyQuestion)questionData;
                            questionTypeSpecificAttributes += "Num_of_faces";
                            questionTypeSpecificValues += $"{smileyQuestionData.NumberOfSmileyFaces}";
                            break;
                        case "Slider":
                            SliderQuestion sliderQuestionData = (SliderQuestion)questionData;
                            questionTypeSpecificAttributes += "Start_value, End_value, Start_value_caption, End_value_caption";
                            questionTypeSpecificValues += $"{sliderQuestionData.StartValue}, {sliderQuestionData.EndValue}," +
                                $" '{sliderQuestionData.StartValueCaption}', '{sliderQuestionData.EndValueCaption}'";
                            break;
                        case "Stars":
                            StarsQuestion starsQuestionData = (StarsQuestion)questionData;
                            questionTypeSpecificAttributes += "Num_of_stars";
                            questionTypeSpecificValues += $"{starsQuestionData.NumberOfStars}";
                            break;
                    }
                    insertQuestionTypeCmd.CommandType = CommandType.Text;
                    insertQuestionTypeCmd.CommandText = $"INSERT INTO {questionType} (Q_id, {questionTypeSpecificAttributes}) VALUES ({questionId}, {questionTypeSpecificValues})";
                    insertQuestionTypeCmd.ExecuteNonQuery();
                    //return question id to add question to UI
                    return questionId;
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

        public static void UpdateQuestionOnDB(int questionId, string originalQuestionType, Question updatedQuestionData)
        {
            try
            {
                //get the original question type and updated question type if it's changed
                string updatedQuestionType = updatedQuestionData.GetType().Name.Split("Q")[0];

                using (SqlConnection conn = new SqlConnection(ConnectionString)) 
                {
                    conn.Open();
                    if (updatedQuestionType.Equals(originalQuestionType))
                    {
                        //type of question wasn't  changed
                        //update the specific details
                        string questionUpdateArguments = "";
                        switch (originalQuestionType)
                        {
                            case "Smiley":
                                SmileyQuestion smileyQuestionData = (SmileyQuestion)updatedQuestionData;
                                questionUpdateArguments += $"Num_of_faces = {smileyQuestionData.NumberOfSmileyFaces}";
                                break;
                            case "Slider":
                                SliderQuestion sliderQuestionData = (SliderQuestion)updatedQuestionData;
                                questionUpdateArguments += $"Start_value = {sliderQuestionData.StartValue}," +
                                    $" End_value ={sliderQuestionData.EndValue}," +
                                    $" Start_value_caption = '{sliderQuestionData.StartValueCaption}'," +
                                    $" End_value_caption = '{sliderQuestionData.EndValueCaption}'";
                                break;
                            case "Stars":
                                StarsQuestion starsQuestionData = (StarsQuestion)updatedQuestionData;
                                questionUpdateArguments += $"Num_of_stars = {starsQuestionData.NumberOfStars}";
                                break;
                        }

                        //create the command to update the question specific data
                        SqlCommand updateQuestionSpecificDataCmd = new SqlCommand();
                        updateQuestionSpecificDataCmd.CommandType = CommandType.Text;
                        updateQuestionSpecificDataCmd.CommandText = $"UPDATE {originalQuestionType} SET {questionUpdateArguments} WHERE Q_id = {questionId}";
                        updateQuestionSpecificDataCmd.Connection = conn;

                        //update the general question
                        SqlCommand updateQuestionDataCmd = conn.CreateCommand();
                        updateQuestionDataCmd.CommandType = CommandType.Text;
                        updateQuestionDataCmd.CommandText = $"UPDATE Question SET Q_order = {updatedQuestionData.Order}, Q_text = @Q_text WHERE Q_id = {questionId}";
                        updateQuestionDataCmd.Parameters.Add(new SqlParameter("@Q_text", updatedQuestionData.Text));
                        updateQuestionDataCmd.Connection = conn;
                        updateQuestionSpecificDataCmd.ExecuteNonQuery();
                        updateQuestionDataCmd.ExecuteNonQuery();
                    }
                    else
                    {
                        //type of question changed
                        //delete the questions specific old data first
                        SqlCommand deleteSpecificQuestionDataCmd = new SqlCommand($"DELETE FROM {originalQuestionType} WHERE Q_id = {questionId}", conn);

                        //update the general question data
                        SqlCommand updateQuestionDataCmd = new SqlCommand
                            ($"UPDATE Question SET Q_order = {updatedQuestionData.Order}, Q_text = @Q_text," +
                            $" Q_type = '{updatedQuestionData.GetType().Name.Split("Q")[0]}' WHERE Q_id = {questionId}",
                            conn);
                        updateQuestionDataCmd.Parameters.Add(new SqlParameter("@Q_text", updatedQuestionData.Text));

                        //create a new row in the specific question type table
                        SqlCommand insertQuestionTypeCmd = conn.CreateCommand();
                        ////get the specific values for the question type
                        string questionTypeSpecificAttributes = "";
                        string questionTypeSpecificValues = "";
                        ////for each type of question downcast the question to its specific type
                        switch (updatedQuestionType)
                        {
                            case "Smiley":
                                SmileyQuestion smileyQuestionData = (SmileyQuestion)updatedQuestionData;
                                questionTypeSpecificAttributes += "Num_of_faces";
                                questionTypeSpecificValues += $"{smileyQuestionData.NumberOfSmileyFaces}";
                                break;
                            case "Slider":
                                SliderQuestion sliderQuestionData = (SliderQuestion)updatedQuestionData;
                                questionTypeSpecificAttributes += "Start_value, End_value, Start_value_caption, End_value_caption";
                                questionTypeSpecificValues += $"{sliderQuestionData.StartValue}, {sliderQuestionData.EndValue}," +
                                    $" '{sliderQuestionData.StartValueCaption}', '{sliderQuestionData.EndValueCaption}'";
                                break;
                            case "Stars":
                                StarsQuestion starsQuestionData = (StarsQuestion)updatedQuestionData;
                                questionTypeSpecificAttributes += "Num_of_stars";
                                questionTypeSpecificValues += $"{starsQuestionData.NumberOfStars}";
                                break;
                        }
                        insertQuestionTypeCmd.CommandType = CommandType.Text;
                        insertQuestionTypeCmd.CommandText = $"INSERT INTO {updatedQuestionType} (Q_id, {questionTypeSpecificAttributes}) VALUES ({questionId}, {questionTypeSpecificValues})";

                        //exectue commands on database
                        deleteSpecificQuestionDataCmd.ExecuteNonQuery();
                        updateQuestionDataCmd.ExecuteNonQuery();
                        insertQuestionTypeCmd.ExecuteNonQuery();
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
                throw new InvalidOperationException("problem in connection to database", ex);
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw;
            }
        }

        public static void DeleteQuestionFromDB(DataRow[] selectedQuestions)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString)) 
                { 
                    conn.Open();
                    SqlCommand deleteQuestionsCmd = conn.CreateCommand();
                    deleteQuestionsCmd.CommandType = CommandType.Text;
                    //delete the specific details of the question type
                    for (int i = 0; i < selectedQuestions.Length; i++)
                    {
                        deleteQuestionsCmd.CommandText = $"DELETE FROM {selectedQuestions[i]["Q_type"]} WHERE Q_id = {selectedQuestions[i]["Q_id"]}";
                        deleteQuestionsCmd.ExecuteNonQuery();
                    }
                    //delete question from database
                    for (int i = 0; i < selectedQuestions.Length; i++)
                    {
                        deleteQuestionsCmd.CommandText = $"DELETE FROM Question WHERE Q_id = {selectedQuestions[i]["Q_id"]}";
                        deleteQuestionsCmd.ExecuteNonQuery();
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
                throw new InvalidOperationException("problem in connection to database",ex);
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw;
            }
        }

        //move to logic layer ?
        public static void LogError(Exception exceptionData)
        {
            try
            {
                //collect the error info to log to the file
                string[] exceptionDetails = [
                    $"{DateTime.Now.ToUniversalTime()} UTC",
                    $"Exception: {exceptionData.GetType().Name}",
                    $"Exception message: {exceptionData.Message}",
                    $"Stack trace:\n{exceptionData.StackTrace}"];
                //check that file exists
                string directoryPath = Directory.GetCurrentDirectory() + "\\errorlogs";
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                string filePath = directoryPath + "\\errorlog.txt";
                if (!File.Exists(filePath))
                {
                    //create the file if it doesn't exist
                    FileStream fs = File.Create(filePath);
                    fs.Close();
                }

                //add the default values to the file
                StreamWriter writer = File.AppendText(filePath);
                writer.WriteLine(string.Join(",\n", exceptionDetails) + "\n\n--------\n");
                writer.Close();
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error occurred while logging: {ex.Message}");
            }
        }

        public static long getChecksum()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand getChecksum = new SqlCommand("SELECT CHECKSUM_AGG(BINARY_CHECKSUM(*)) FROM Question WITH (NOLOCK)", conn);
                    long checksum = (int)getChecksum.ExecuteScalar();
                    return checksum;
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
    }
}
