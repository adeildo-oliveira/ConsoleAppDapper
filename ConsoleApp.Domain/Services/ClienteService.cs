using ConsoleApp.Domain.Interfaces.Repository;
using ConsoleApp.Domain.Interfaces.Services;
using ConsoleApp.Domain.Models;
using System.Threading.Tasks;

namespace ConsoleApp.Domain.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly INotificationHandlerService _notificationHandlerService;

        public ClienteService(IClienteRepository clienteRepository, INotificationHandlerService notificationHandlerService)
        {
            _clienteRepository = clienteRepository;
            _notificationHandlerService = notificationHandlerService;
        }

        public async Task InserirCliente(Cliente cliente)
        {
            if (cliente != null)
                await _clienteRepository.InserirCliente(cliente);
            else
                _notificationHandlerService.AddDomainNotification(new NotificationHandler("Cliente", "Cliente inválido"));
        }

        public void Dispose()
        {
            _clienteRepository.Dispose();
            _notificationHandlerService.Dispose();
        }
    }
}
