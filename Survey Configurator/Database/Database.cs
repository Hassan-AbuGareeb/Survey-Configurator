using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using SharedResources.models;
using Microsoft.Data.SqlClient;
using SharedResources;

namespace DatabaseLayer
{
    public class Database
    {
        public static string ConnectionString;

        private Database() { }
        #region class main functions
        public static void getQuestionsFromDB(ref List<Question> pQuestionsList)
        {
            using (SqlConnection tConn = new SqlConnection(ConnectionString)) 
            {
                tConn.Open();
                SqlCommand tGetQuestionsDataCmd = new SqlCommand("SELECT * FROM Question", tConn);
                DbDataReader tReader = tGetQuestionsDataCmd.ExecuteReader(CommandBehavior.CloseConnection);
                //iterate over each row in the Reader and add it to the questions list
                while (tReader.Read())
                {
                    pQuestionsList.Add(new Question((int)tReader["Id"], tReader["Text"].ToString(),
                        (int)tReader["order"], (SharedResources.QuestionType)tReader["Type"]));
                }
                tReader.Close();
            }
        }

        public static Question getQuestionSpecificDataFromDB(Question pQuestionData)
        {
            using (SqlConnection tConn = new SqlConnection(ConnectionString)) 
            {
                tConn.Open();
                //parameterize the query
                string tQuestionType = pQuestionData.Type.ToString();

                SqlCommand tGetQuestionSpecificData = new SqlCommand($"SELECT * FROM {tQuestionType} WHERE Id = @Id", tConn);
                tGetQuestionSpecificData.Parameters.Add(new SqlParameter("@Id",pQuestionData.Id));
                DbDataReader tReader = tGetQuestionSpecificData.ExecuteReader(CommandBehavior.CloseConnection);
                Question tSpecificQuestionData= null;
                while(tReader.Read())
                {
                    switch(pQuestionData.Type)
                    {
                        case SharedData.cStarsType:
                            tSpecificQuestionData = new StarsQuestion(pQuestionData, (int)tReader["NumberOfStars"]);
                            break;
                        case SharedData.cSmileyType:
                            tSpecificQuestionData = new SmileyQuestion(pQuestionData, (int)tReader["NumberOfFaces"]);
                            break;
                        case SharedData.cSliderType:
                            tSpecificQuestionData = new SliderQuestion(pQuestionData, 
                                (int)tReader["StartValue"], (int)tReader["EndValue"],
                                tReader["StartValueCaption"].ToString(), tReader["EndValueCaption"].ToString());
                            break;
                    }
                }
                tReader.Close();
                return tSpecificQuestionData;
            }
        }

        public static Question AddQuestionToDB(Question pQuestionData)
        {
            //create db connection
            using(SqlConnection tConn = new SqlConnection(ConnectionString))
            {
                tConn.Open();
                //insert question data in the question table
                SqlCommand tInsertQuestionCmd = new SqlCommand($"INSERT INTO Question (Q_text, Q_order, Q_type) OUTPUT INSERTED.Q_id VALUES (@Q_text, {pQuestionData.Order}, '{pQuestionData.Type.ToString()}')",
                    tConn);
                tInsertQuestionCmd.Parameters.Add(new SqlParameter("@Q_text", pQuestionData.Text));

                //insert the row data to the question and return the id of the created question
                int tQuestionId = (int)tInsertQuestionCmd.ExecuteScalar();
                    
                //get the specific values for the question type
                string tQuestionTypeSpecificAttributes = "";
                string tQuestionTypeSpecificValues = "";
                //for each type of question downcast the question to its specific type and obtain its properties
                switch (pQuestionData.Type)
                {
                    case SharedData.cSmileyType:
                        SmileyQuestion smileyQuestionData = (SmileyQuestion)pQuestionData;
                        tQuestionTypeSpecificAttributes += "Num_of_faces";
                        tQuestionTypeSpecificValues += $"{smileyQuestionData.NumberOfSmileyFaces}";
                        break;
                    case SharedData.cSliderType:
                        SliderQuestion sliderQuestionData = (SliderQuestion)pQuestionData;
                        tQuestionTypeSpecificAttributes += "Start_value, End_value, Start_value_caption, End_value_caption";
                        tQuestionTypeSpecificValues += $"{sliderQuestionData.StartValue}, {sliderQuestionData.EndValue}," +
                            $" '{sliderQuestionData.StartValueCaption}', '{sliderQuestionData.EndValueCaption}'";
                        break;
                    case SharedData.cStarsType:
                        StarsQuestion starsQuestionData = (StarsQuestion)pQuestionData;
                        tQuestionTypeSpecificAttributes += "Num_of_stars";
                        tQuestionTypeSpecificValues += $"{starsQuestionData.NumberOfStars}";
                        break;
                }
                SqlCommand tInsertQuestionTypeCmd = new SqlCommand($"INSERT INTO {pQuestionData.Type.ToString()} (Q_id, {tQuestionTypeSpecificAttributes}) VALUES ({tQuestionId}, {tQuestionTypeSpecificValues})",
                    tConn);
                tInsertQuestionTypeCmd.ExecuteNonQuery();
                //return question id to add question to UI
                return new Question (tQuestionId, pQuestionData.Text, pQuestionData.Order, pQuestionData.Type);
            }
        }

        public static void UpdateQuestionOnDB(string pOriginalQuestionType, Question pUpdatedQuestionData)
        {
           string pUpdatedQuestionType = pUpdatedQuestionData.GetType().Name.Split("Q")[0];

           using (SqlConnection conn = new SqlConnection(ConnectionString)) 
            {
                conn.Open();
                if (pUpdatedQuestionType.Equals(pOriginalQuestionType))
                {
                    //type of question wasn't  changed
                    //update the specific details
                    string questionUpdateArguments = "";
                    SqlCommand updateQuestionSpecificDataCmd = conn.CreateCommand();
                    switch (pOriginalQuestionType)
                    {
                        case "Smiley":
                            SmileyQuestion smileyQuestionData = (SmileyQuestion)pUpdatedQuestionData;
                            questionUpdateArguments += $"Num_of_faces = {smileyQuestionData.NumberOfSmileyFaces}";
                            break;
                        case "Slider":
                            SliderQuestion sliderQuestionData = (SliderQuestion)pUpdatedQuestionData;
                            questionUpdateArguments += $"Start_value = {sliderQuestionData.StartValue}," +
                                $" End_value = {sliderQuestionData.EndValue}," +
                                $" Start_value_caption = @Start_value_caption," +
                                $" End_value_caption = @End_value_caption";

                            updateQuestionSpecificDataCmd.Parameters.Add(new SqlParameter("@Start_value_caption", sliderQuestionData.StartValueCaption));
                            updateQuestionSpecificDataCmd.Parameters.Add(new SqlParameter("@End_value_caption", sliderQuestionData.EndValueCaption));
                            break;
                        case "Stars":
                            StarsQuestion starsQuestionData = (StarsQuestion)pUpdatedQuestionData;
                            questionUpdateArguments += $"Num_of_stars = {starsQuestionData.NumberOfStars}";
                            break;
                    }
                    updateQuestionSpecificDataCmd.CommandType = CommandType.Text;
                    updateQuestionSpecificDataCmd.CommandText = $"UPDATE {pOriginalQuestionType} SET {questionUpdateArguments} WHERE Q_id = {pUpdatedQuestionData.Id}";

                    //update the general question
                    SqlCommand updateQuestionDataCmd = new SqlCommand($"UPDATE Question SET Q_order = {pUpdatedQuestionData.Order}, Q_text = @Q_text WHERE Q_id = {pUpdatedQuestionData.Id}",
                        conn);
                    updateQuestionDataCmd.Parameters.Add(new SqlParameter("@Q_text", pUpdatedQuestionData.Text));

                    updateQuestionSpecificDataCmd.ExecuteNonQuery();
                    updateQuestionDataCmd.ExecuteNonQuery();
                }
                else
                {
                    //type of question changed
                    //delete the questions specific old data first
                    SqlCommand deleteSpecificQuestionDataCmd = new SqlCommand($"DELETE FROM {pOriginalQuestionType} WHERE Q_id = {pUpdatedQuestionData.Id}", conn);

                    //update the general question data
                    SqlCommand updateQuestionDataCmd = new SqlCommand
                        ($"UPDATE Question SET Q_order = {pUpdatedQuestionData.Order}, Q_text = @Q_text," +
                        $" Q_type = '{pUpdatedQuestionData.GetType().Name.Split("Q")[0]}' WHERE Q_id = {pUpdatedQuestionData.Id}",
                        conn);
                    updateQuestionDataCmd.Parameters.Add(new SqlParameter("@Q_text", pUpdatedQuestionData.Text));

                    //create a new row in the specific question type table
                    SqlCommand insertQuestionTypeCmd = conn.CreateCommand();
                    //get the specific values for the question type
                    string questionTypeSpecificAttributes = "";
                    string questionTypeSpecificValues = "";

                    //for each type of question downcast the question to its specific type
                    switch (pUpdatedQuestionType)
                    {
                        case "Smiley":
                            SmileyQuestion smileyQuestionData = (SmileyQuestion)pUpdatedQuestionData;
                            questionTypeSpecificAttributes += "Num_of_faces";
                            questionTypeSpecificValues += $"{smileyQuestionData.NumberOfSmileyFaces}";
                            break;
                        case "Slider":
                            SliderQuestion sliderQuestionData = (SliderQuestion)pUpdatedQuestionData;
                            questionTypeSpecificAttributes += "Start_value, End_value, Start_value_caption, End_value_caption";
                            questionTypeSpecificValues += $"{sliderQuestionData.StartValue}, {sliderQuestionData.EndValue}," +
                                $" @Start_value_caption, @End_value_caption";
                            //to ensure safety against sql injection
                            insertQuestionTypeCmd.Parameters.Add(new SqlParameter("@Start_value_caption", sliderQuestionData.StartValueCaption));
                            insertQuestionTypeCmd.Parameters.Add(new SqlParameter("@End_value_caption", sliderQuestionData.EndValueCaption));
                            break;
                        case "Stars":
                            StarsQuestion starsQuestionData = (StarsQuestion)pUpdatedQuestionData;
                            questionTypeSpecificAttributes += "Num_of_stars";
                            questionTypeSpecificValues += $"{starsQuestionData.NumberOfStars}";
                            break;
                    }

                    insertQuestionTypeCmd.CommandType = CommandType.Text;
                    insertQuestionTypeCmd.CommandText = $"INSERT INTO {pUpdatedQuestionType} (Q_id, {questionTypeSpecificAttributes}) VALUES ({pUpdatedQuestionData.Id}, {questionTypeSpecificValues})";

                    //exectue commands on database
                    deleteSpecificQuestionDataCmd.ExecuteNonQuery();
                    updateQuestionDataCmd.ExecuteNonQuery();
                    insertQuestionTypeCmd.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteQuestionFromDB(Question[] pSelectedQuestions)
        {
            using (SqlConnection tConn = new SqlConnection(ConnectionString)) 
            {
                tConn.Open();
                SqlCommand tDeleteQuestionsCmd = tConn.CreateCommand();
                tDeleteQuestionsCmd.CommandType = CommandType.Text;
                //delete the specific details of the question type
                for (int i = 0; i < pSelectedQuestions.Length; i++)
                {
                    Question tCurrentQuestion = pSelectedQuestions[i];
                    tDeleteQuestionsCmd.CommandText = $"DELETE FROM {tCurrentQuestion.Type} WHERE Id = @Id";
                    tDeleteQuestionsCmd.Parameters.Add(new SqlParameter("@Id",tCurrentQuestion.Id));
                    tDeleteQuestionsCmd.ExecuteNonQuery();
                    tDeleteQuestionsCmd.Parameters.Clear();
                }

                //delete question from database
                for (int i = 0; i < pSelectedQuestions.Length; i++)
                {
                    Question tCurrentQuestion = pSelectedQuestions[i];
                    tDeleteQuestionsCmd.CommandText = $"DELETE FROM Question WHERE Id = @Id";
                    tDeleteQuestionsCmd.Parameters.Add(new SqlParameter("@Id", tCurrentQuestion.Id));

                    tDeleteQuestionsCmd.ExecuteNonQuery();
                }
            }
        }
        #endregion

        #region class utility functions
        public static long getChecksum()
        {
            //better handling for the null case
            using (SqlConnection tConn = new SqlConnection(ConnectionString))
                {
                    tConn.Open();
                    SqlCommand tGetChecksum = new SqlCommand("SELECT CHECKSUM_AGG(BINARY_CHECKSUM(*)) FROM Question WITH (NOLOCK)", tConn);
                    var tChecksum = tGetChecksum.ExecuteScalar();
                    if (DBNull.Value.Equals(tChecksum))
                        return 0;
                    return (int)tChecksum;
                }
        }
        #endregion
    }
}
