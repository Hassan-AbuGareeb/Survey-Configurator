using Database.models;
using Microsoft.Data.SqlClient;
using System.Data;


namespace Logic
{
    public class QuestionOperations
    {
        // make a general purpose open connection function and send delegates to it


        private static DataTable Questions;
        private static SqlConnection conn;
        public static string ConnectionString="" ;
        private QuestionOperations() 
        {
        }

        public static DataTable getQuestions() 
        {
            Questions = Database.Database.getQuestionsFromDB();
            return Questions;
        }

        public static DataRow getQuestionData(int questionId)
        {
            DataRow questionGeneralData = Questions.Select($"Q_id = {questionId}")[0];
            return questionGeneralData;
        }

        public static DataRow getQuestionSpecificData(int questionId, string questionType)
        {
            return Database.Database.getQuestionSpecificDataFromDB(questionId, questionType);
        }

        public static void AddQuestion(Question questionData)
        {
           int questionId =Database.Database.AddQuestionToDB(questionData);
           string questionType = questionData.GetType().Name.Split('Q')[0];
           Questions.Rows.Add(questionId, questionData.Text.Replace("''","'"), questionData.Order, questionType);
        }

        public static void UpdateQuestion(int questionId, Question updatedQuestionData)
        {
            //recieve the new question general and specific data
            string originalQuestionType = Questions.Select($"Q_id = {questionId}")[0]["Q_type"].ToString();
            string updatedQuestionType = Database.Database.UpdateQuestionOnDB( questionId, originalQuestionType, updatedQuestionData);
            Questions.Rows.Remove(Questions.Select($"Q_id = {questionId}")[0]);
            Questions.Rows.Add(questionId, updatedQuestionData.Text.Replace("''", "'"), updatedQuestionData.Order, updatedQuestionType);

        }

        public static void DeleteQuestion(DataRow[] selectedQuestions)
        {
            Database.Database.DeleteQuestionFromDB(selectedQuestions);
            //delete question from interface (Questions) in here
            for (int i = 0; i < selectedQuestions.Length; i++)
            {
                Questions.Rows.Remove(selectedQuestions[i]);
            }
        }

        public static void setConnectionString(string connectionString)
        {
            Database.Database.ConnectionString = connectionString;
        }
    }
}
