using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace ConsoleApp.InfraData.Context
{
    public class GetConnectionDapper
    {
        protected IDbConnection Connection => new SqlConnection(
            new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build()
            .GetConnectionString("DefaultConnection"));
    }
}
