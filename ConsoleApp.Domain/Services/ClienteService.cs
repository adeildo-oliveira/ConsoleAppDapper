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
            if (cliente == null)
                _notificationHandlerService.AddNotification(new NotificationHandler("Cliente", "Cliente inválido"));

            if (!_notificationHandlerService.HasNotifications() && cliente?.EValido() == true)
                await _clienteRepository.InserirCliente(cliente);
            else
                _notificationHandlerService.AddValidationResult(cliente?.ValidationResult);
        }

        public void Dispose()
        {
            _clienteRepository.Dispose();
            _notificationHandlerService.Dispose();
        }
    }
}
