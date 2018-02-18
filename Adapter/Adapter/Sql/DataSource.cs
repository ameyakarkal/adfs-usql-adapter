using System.Data;
using System.Data.SqlClient;

namespace Adapter.Sql
{
    public class DataSource
    {
        private readonly string _connectionString;

        public DataSource(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
