using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using WindowsServiceDemo.Entities;

namespace WindowsServiceDemo.DataAccess
{
    public class TsContext : DbContext
    {
        private readonly string _connectionString;

        public TsContext()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["TsContext"].ConnectionString;
        }

        public DbSet<Car> Cars { get; set; }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}