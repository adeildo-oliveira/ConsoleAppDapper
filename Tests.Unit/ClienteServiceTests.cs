using ConsoleApp.Domain.Interfaces.Repository;
using ConsoleApp.Domain.Interfaces.Services;
using ConsoleApp.Domain.Models;
using ConsoleApp.Domain.Services;
using FluentValidation.Results;
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
        private readonly Mock<INotificationHandlerService> _notificationMock;

        public ClienteServiceTests()
        {
            _mocker = new AutoMocker();
            _notificationMock = new Mock<INotificationHandlerService>();

            _service = new ClienteService(
                _mocker.GetMock<IClienteRepository>().Object,
                _notificationMock.Object);
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
            _notificationMock.Setup(x => x.HasNotifications()).Returns(false);

            await _service.InserirCliente(clienteBuilder);

            _mocker.Verify<IClienteRepository>(x => x.InserirCliente(clienteBuilder), Times.Once);
            _notificationMock.Verify(x => x.AddValidationResult(It.IsAny<ValidationResult>()), Times.Never);
            _notificationMock.Verify(x => x.HasNotifications(), Times.Once);
            _notificationMock.Verify(x => x.AddNotification(It.IsAny<NotificationHandler>()), Times.Never);
            _notificationMock.Verify(x => x.RemoveNotification(It.IsAny<NotificationHandler>()), Times.Never);
        }

        [Fact]
        public async Task NaoDeveInserirOClienteERetornarUmaMensagemDeValidacao()
        {
            var notifications = new List<NotificationHandler>();

            _mocker.GetMock<IClienteRepository>().Setup(x => x.InserirCliente(null)).Returns(Task.CompletedTask);
            _notificationMock.Setup(x => x.HasNotifications()).Returns(true);
            _notificationMock.Setup(x => x.AddNotification(It.IsAny<NotificationHandler>()))
                .Callback(() => 
                {
                    notifications.Add(new NotificationHandlerBuilder()
                        .ComKey("Cliente")
                        .ComValue("Cliente inválido")
                        .Instanciar());
                });
            _notificationMock.Setup(x => x.AddValidationResult(It.IsAny<ValidationResult>()));

            await _service.InserirCliente(null);

            _mocker.Verify<IClienteRepository>(x => x.InserirCliente(null), Times.Never);
            _notificationMock.Verify(x => x.AddNotification(It.IsAny<NotificationHandler>()), Times.Once);
            _notificationMock.Verify(x => x.AddValidationResult(It.IsAny<ValidationResult>()), Times.Once);
            _notificationMock.Verify(x => x.HasNotifications(), Times.Once);
            _notificationMock.Verify(x => x.AddNotification(It.IsAny<NotificationHandler>()), Times.Once);
            _notificationMock.Verify(x => x.RemoveNotification(It.IsAny<NotificationHandler>()), Times.Never);
        }

        [Fact]
        public async Task NaoDeveInserirOClienteERetornarUmaMensagemDeValidacaoQuandoNomeOuSobreNomeNaoForemInformados()
        {
            var notifications = new List<NotificationHandler>();

            var clienteBuilder = new ClienteBuilder()
                .ComId(Guid.Empty)
                .Instanciar();

            _mocker.GetMock<IClienteRepository>().Setup(x => x.InserirCliente(clienteBuilder)).Returns(Task.CompletedTask);
            _notificationMock.Setup(x => x.HasNotifications()).Returns(true);
            _notificationMock.Setup(x => x.AddValidationResult(It.IsAny<ValidationResult>()));

            await _service.InserirCliente(clienteBuilder);

            _mocker.Verify<IClienteRepository>(x => x.InserirCliente(clienteBuilder), Times.Never);
            _notificationMock.Verify(x => x.AddValidationResult(It.IsAny<ValidationResult>()), Times.Once);
            _notificationMock.Verify(x => x.HasNotifications(), Times.Once);
            _notificationMock.Verify(x => x.RemoveNotification(It.IsAny<NotificationHandler>()), Times.Never);
            _notificationMock.Verify(x => x.AddNotification(It.IsAny<NotificationHandler>()), Times.Never);
        }
    }
}
