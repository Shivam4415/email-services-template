using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Emails
{
    public class SmtpServer
    {
        public string Name { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsDefault { get; set; }
        public string FromAddress { get; set; }

        public static int ComparerThatMovesDefaultServersOnTop(SmtpServer first, SmtpServer second)
        {
            if (first.IsDefault == second.IsDefault)
                return 0;
            if (first.IsDefault == true)
                return -1;

            return 1;
        }
    }

}