using ConsoleApp.Domain.Interfaces.Repository;
using ConsoleApp.Domain.Interfaces.Services;
using ConsoleApp.Domain.Services;
using ConsoleApp.InfraData;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ConsoleAppDapper
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddScoped<IClienteRepository, ClienteRepository>()
                .AddScoped<IClienteService, ClienteService>()
                .AddScoped<INotificationHandlerService, NotificationHandlerService>()
                .BuildServiceProvider();

            Console.ReadKey();
        }
    }
}
