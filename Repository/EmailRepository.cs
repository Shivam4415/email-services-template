using Dapper;
using Library.Global;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Emails
{
    public class EmailRepository
    {
        public bool IsEmailBlackListed(string email)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(DbConnectionString.App))
                {
                    return con.Query<bool>("[gapsnap].[ValidateIfEmailBlackListed]", new { email = email }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
            }
            catch
            {
                throw;
            }
        }       

    }
}