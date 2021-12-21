using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Library.Global;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace Library.Emails
{
   internal class SMTPServerRepository
    {
        /// <summary>
        /// Get SMTP server list from database - static date
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SmtpServer> GetAll()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(DbConnectionString.App))
                {
                    return con.Query<SmtpServer>("[gapsnap].[GetSmtpServers]", commandType: CommandType.StoredProcedure);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}