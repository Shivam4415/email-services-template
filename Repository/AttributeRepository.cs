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
    internal class AttributeRepository
    {
  
        /// <summary>
        /// get List of EmailAttributes - static data
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Attribute> GetAll()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(DbConnectionString.App))
                {
                    return con.Query<Attribute>("[gapsnap].[GetEmailAttributes]", commandType: CommandType.StoredProcedure);
                }
            }
            catch
            {
                throw;
            }
        }

    }
}