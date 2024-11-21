using Microsoft.Data.SqlClient;
using System.Data;

namespace OnlineFoodDeliverySystemAPI.Data
{
    public class DatabaseHelper
    {
        public readonly string _ConnectionString;
        public DatabaseHelper(string connectionString)
        {
            _ConnectionString = connectionString;
        }
        public DataTable ExecuteQuery(string query)
        {
            using (SqlConnection conn = new SqlConnection(_ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        var datable = new DataTable();
                        adapter.Fill(datable);
                        return datable;
                    }
                }
            }
        }

    }
}


