using ConsoleApp.Domain.Interfaces.Repository;
using ConsoleApp.Domain.Interfaces.Services;
using ConsoleApp.Domain.Models;
using ConsoleApp.Domain.Services;
using ConsoleApp.InfraData;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace ConsoleAppDapper
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var services = new ServiceCollection()
                .AddScoped<IClienteRepository, ClienteRepository>()
                .AddScoped<IClienteService, ClienteService>()
                .AddScoped<INotificationHandlerService, NotificationHandlerService>();
            await CallServices(services.BuildServiceProvider());
        }

        public static async Task CallServices(ServiceProvider services)
        {
            Console.Write("Nome: ");
            var nome = Console.ReadLine();

            Console.Write("Sobre Nome: ");
            var sobreNome = Console.ReadLine();

            var id = Guid.NewGuid();
            var cliente = new Cliente(id, nome, sobreNome);
            await services.GetService<IClienteService>().InserirCliente(cliente);

            var resultado = await services.GetService<IClienteRepository>().ObterCliente(id);
            if(resultado != null)
            {
                Console.WriteLine("Dados Cadastrado na Base:");
                Console.WriteLine($"\nDados Cliente: {nameof(resultado.Id)}: {resultado.Id} {nameof(resultado.Nome)}: {resultado.Nome} {nameof(resultado.SobreNome)}: {resultado.SobreNome}");
            }

            foreach (var item in services.GetService<INotificationHandlerService>().GetNotifications())
                Console.WriteLine($"Domain Notification: {item.Value}");
        }
    }
}
