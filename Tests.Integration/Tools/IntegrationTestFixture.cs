using ConsoleApp.InfraData;
using ConsoleApp.InfraData.Context;
using Dapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data;

namespace Tests.Integration.Tools
{
    public class IntegrationTestFixture : IDisposable
    {
        private readonly ServiceCollection _serviceCollection;
        private readonly ServiceProvider _serviceProvider;

        public IntegrationTestFixture()
        {

            _serviceCollection = _serviceCollection ?? new ServiceCollection();
            _serviceCollection.AddScoped<IClienteRepository, ClienteRepository>();

            _serviceProvider = _serviceCollection.BuildServiceProvider();   
        }

        public T GetService<T>()
        {
            ClearDataBase();
            return _serviceProvider.GetService<T>();
        }

        private void ClearDataBase()
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
            GC.SuppressFinalize(this);
        }
    }
}
