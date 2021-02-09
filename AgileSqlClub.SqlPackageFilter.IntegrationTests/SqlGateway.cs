using System.Data.SqlClient;

namespace AgileSqlClub.SqlPackageFilter.IntegrationTests
{
    class SqlGateway
    {
        private readonly  string _connectionString;

        public readonly string dbServer;

        public readonly string dbName;

        public SqlGateway(string connectionString)
        {
            _connectionString = connectionString;
            var dbParts = new SqlConnectionStringBuilder(_connectionString);


            dbServer = dbParts.DataSource;
            dbName = dbParts.InitialCatalog;
        }

        public void RunQuery(string query)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();

                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public int GetInt(string query)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();

                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = query;
                    var result = cmd.ExecuteScalar();
                    return (int)result;
                }
            }
        }
    }
}
