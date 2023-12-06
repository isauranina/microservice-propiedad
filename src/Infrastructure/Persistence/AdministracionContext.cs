
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace Infrastructure.Persistence
{

    public class AdministracionContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public AdministracionContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("ConexionAdmin");
        }
        public IDbConnection CreateConnection()
            => new NpgsqlConnection(_connectionString);
    }
}
