﻿namespace SharedResources
{
    public class ConnectionString
    {
        /// <summary>
        /// a class to facilitate the process of obtaining the connection string
        /// and changing/saving it in the connectionString.json file
        /// </summary>
        public string Server {  get; set; }
        public string Database { get; set; }
        public bool Trusted_Connection { get; set; }
        public string User {  get; set; }
        public string Password { get; set; }
        public bool Encrypt { get; set; }
        public int Timeout { get; set; }

        public ConnectionString() {
            try { 
                Server = string.Empty;
                Database = string.Empty;
                Trusted_Connection = false;
                User = string.Empty;
                Password = string.Empty;
                Encrypt = false;
                Timeout = 5;
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
                return $"Server={Server}; Database={Database}; Trusted_Connection={Trusted_Connection}; User={User}; Password={Password}; Encrypt={Encrypt}; Timeout={Timeout}";
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
                return string.Empty;
            }
        }

    }
}

