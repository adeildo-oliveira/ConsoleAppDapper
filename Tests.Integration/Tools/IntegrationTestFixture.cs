using ConsoleApp.Domain.Interfaces.Repository;
using ConsoleApp.InfraData;
using ConsoleApp.InfraData.Context;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace Tests.Integration.Tools
{
    public class IntegrationTestFixture : IDisposable
    {
        private readonly ServiceCollection _serviceCollection;
        private readonly ServiceProvider _serviceProvider;
        private static string connectionString;

        public IntegrationTestFixture()
        {

            _serviceCollection = _serviceCollection ?? new ServiceCollection();
            _serviceCollection.AddScoped<IClienteRepository, ClienteRepository>();

            _serviceProvider = _serviceCollection.BuildServiceProvider();

            connectionString = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build()
                .GetConnectionString("DefaultConnection")
                .Replace("AppConsoleDapper_Integration", "Master");

            if (!ExisteDataBase())
                CreateDataBase();

            if(!ExisteTables())
                CreateTables();
        }

        public T GetService<T>()
        {
            ClearTables();
            return _serviceProvider.GetService<T>();
        }

        private static bool ExisteDataBase()
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var resultado = conn.QueryFirstOrDefault($"SELECT name as Nome FROM master.dbo.sysdatabases where name = '{GetConnectionDapper.Connection.Database}'", 
                    commandType: CommandType.Text);

                return resultado == null ? false : true;
            }
        }

        private static void CreateDataBase()
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                conn.Query($"CREATE DATABASE {GetConnectionDapper.Connection.Database}", commandType: CommandType.Text);
            }
        }

        private static bool ExisteTables()
        {
            using (var conn = GetConnectionDapper.Connection)
            {
                conn.Open();
                var resultado = conn.QueryFirstOrDefault(
                    $"SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_CATALOG = '{GetConnectionDapper.Connection.Database}'",
                    commandType: CommandType.Text);

                return resultado == null ? false : true;
            }
        }

        private static void CreateTables()
        {
            var file = Directory.GetFiles($"{Environment.CurrentDirectory}\\Tools").FirstOrDefault(x => x.Contains(".sql"));
            var scriptSQL = File.ReadAllText(file);

            using (var conn = GetConnectionDapper.Connection)
            {
                conn.Open();
                conn.Query(scriptSQL, commandType: CommandType.Text);
            }
        }

        private static void ClearTables()
        {
            using (var conn = GetConnectionDapper.Connection)
            {
                var script = @"EXEC sp_msforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT all'
                               EXEC sp_msforeachtable 'DELETE FROM ?'
                               EXEC sp_msforeachtable 'ALTER TABLE ? CHECK CONSTRAINT all'";

                conn.Open();
                conn.Query(script, commandType: CommandType.Text);
            }
        }

        public void Dispose()
        {
            _serviceProvider.Dispose();
            _serviceCollection.Clear();
            GetConnectionDapper.Connection.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
