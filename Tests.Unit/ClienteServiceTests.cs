using ConsoleApp.Domain.Interfaces.Repository;
using ConsoleApp.Domain.Interfaces.Services;
using ConsoleApp.Domain.Models;
using ConsoleApp.Domain.Services;
using Moq;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tests.Shared;
using Xunit;

namespace Tests.Unit
{
    public class ClienteServiceTests
    {
        private readonly AutoMocker _mocker;
        private readonly ClienteService _service;

        public ClienteServiceTests()
        {
            _mocker = new AutoMocker();
            _service = new ClienteService(
                _mocker.GetMock<IClienteRepository>().Object, 
                _mocker.GetMock<INotificationHandlerService>().Object);
        }

        [Fact]
        public async Task DeveInserirUmClienteCorretamente()
        {
            var clienteBuilder = new ClienteBuilder()
                .ComId(Guid.NewGuid())
                .ComNome("Test")
                .ComSobreNome("Develomper")
                .Instanciar();

            _mocker.GetMock<IClienteRepository>().Setup(x => x.InserirCliente(clienteBuilder)).Returns(Task.CompletedTask);

            await _service.InserirCliente(clienteBuilder);
            _mocker.Verify<IClienteRepository>(x => x.InserirCliente(clienteBuilder), Times.Once);
            _mocker.Verify<INotificationHandlerService>(x => x.AddDomainNotification(It.IsAny<NotificationHandler>()), Times.Never);
            _mocker.Verify<INotificationHandlerService>(x => x.RemoveNotification(It.IsAny<NotificationHandler>()), Times.Never);
        }

        [Fact]
        public async Task NaoDeveInserirOClienteERetornarUmaMensagemDeValidacao()
        {
            var notifications = new List<NotificationHandler>();

            _mocker.GetMock<IClienteRepository>().Setup(x => x.InserirCliente(null)).Returns(Task.CompletedTask);
            _mocker.GetMock<INotificationHandlerService>().Setup(x => x.AddDomainNotification(It.IsAny<NotificationHandler>()))
                .Callback(() => 
                {
                    notifications.Add(new NotificationHandlerBuilder()
                        .ComKey("Cliente")
                        .ComValue("Cliente inválido")
                        .Instanciar());
                });

            await _service.InserirCliente(null);

            _mocker.Verify<IClienteRepository>(x => x.InserirCliente(null), Times.Never);
            _mocker.Verify<INotificationHandlerService>(x => x.AddDomainNotification(It.IsAny<NotificationHandler>()), Times.Once);
            _mocker.Verify<INotificationHandlerService>(x => x.RemoveNotification(It.IsAny<NotificationHandler>()), Times.Never);
        }
    }
}
