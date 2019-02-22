using Microsoft.Extensions.DependencyInjection;
using System;

namespace Tests.Integration.Tools
{
    public class IntegrationTestFixture : IDisposable
    {
        private readonly ServiceCollection _serviceCollection;

        public IntegrationTestFixture()
        {
            _serviceCollection = _serviceCollection ?? new ServiceCollection();
            ServiceProvider = _serviceCollection.BuildServiceProvider();
        }

        public ServiceProvider ServiceProvider { get; private set; }

        public void Dispose() => GC.SuppressFinalize(this);
    }
}
