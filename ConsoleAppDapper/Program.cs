using ConsoleApp.InfraData;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ConsoleAppDapper
{
    public class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddScoped<IClienteRepository, ClienteRepository>()
                .BuildServiceProvider();

            Console.ReadKey();
        }
    }
}
