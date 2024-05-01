using Database.models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace Logic
{
    public class Class1
    {
        //private static List<Question> Questions = new List<Question>();
        private static DataTable Questions;
        private static SqlConnection conn;
        private Class1() { }

        public static DataTable getQuestions() 
        {
            conn = new SqlConnection("Server=HASSANABUGHREEB;Database=Questions_DB;Trusted_Connection=true;Encrypt=false");
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT Q_order, Q_text, Q_type  FROM Question";
            conn.Open();
            Console.WriteLine("connected");
            DbDataReader reader = cmd.ExecuteReader();
            Questions = new DataTable();
            Questions.Load(reader, LoadOption.Upsert);
            conn.Close();
            return Questions;
        }
    }
}
