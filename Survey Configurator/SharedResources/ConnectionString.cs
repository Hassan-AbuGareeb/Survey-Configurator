using Microsoft.Data.SqlClient;

namespace SharedResources
{
    public class ConnectionString
    {
        /// <summary>
        /// a class to facilitate the process of obtaining the connection string
        /// and changing/saving it in the connectionString.json file
        /// </summary>
        public string Server {  get; set; }
        public string Database { get; set; }
        public string User {  get; set; }
        public string Password { get; set; }
        public bool Encrypt { get; set; }
        public int Timeout { get; set; }
        public bool IntegratedSecurity { get; set; }

        public ConnectionString() {
            try { 
                Server = string.Empty;
                Database = string.Empty;
                User = string.Empty;
                Password = string.Empty;
                Encrypt = false;
                Timeout = 5;
                IntegratedSecurity = true;
            }
            catch(Exception ex)
            {
                UtilityMethods.LogError(ex);
            }
        }

        /// <summary>
        /// return a connection string in its correct format
        /// </summary>
        /// <returns>correctly formatted connection string</returns>
        public string GetFormattedConnectionString()
        {
            try
            {
                return $"Server={Server}; Database={Database}; Integrated Security={IntegratedSecurity}; User={User}; Password={Password}; Encrypt={Encrypt}; Timeout={Timeout}";
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
                return string.Empty;
            }
        }

    }
}

