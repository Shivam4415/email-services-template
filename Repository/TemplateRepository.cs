using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using Library.Global;
using System.Collections.Generic;

namespace Library.Emails
{
    internal class TemplateRepository
    {
        /// <summary>
        /// Get Email Templates from database -- static data
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Template> GetAll()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(DbConnectionString.App))
                {

                    return con.Query<Template>("[gapsnap].[GetEmailTemplates]", commandType: CommandType.StoredProcedure);
                }
            }
            catch
            {
                throw;
            }
        }


    }
}