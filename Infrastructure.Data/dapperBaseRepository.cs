using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Infrastructure.Data
{
    public class dapperBaseRepository
    {
        private static readonly string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["RefBookDb"].ConnectionString;

        protected SqlConnection GetConnection(bool open = true)
        {
            var conn =  new SqlConnection(connectionString);

            if (open)
                conn.Open();
            return conn;
            
        }
    }
}
