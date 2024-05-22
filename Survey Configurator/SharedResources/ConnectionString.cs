using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedResources
{
    public class ConnectionString
    {
        public string Server {  get; set; }
        public string Database { get; set; }
        public bool Trusted_Connection { get; set; }
        public string User {  get; set; }
        public string Password { get; set; }
        public bool Encrypt { get; set; }
        public int Timeout { get; set; }

        public ConnectionString() {
            Server = string.Empty;
            Database = string.Empty;
            Trusted_Connection = false;
            User = string.Empty;
            Password = string.Empty;
            Encrypt = false;
            Timeout = 5;
        }

    }
}

