using ConsoleApp.Domain.Services;
using FluentAssertions;
using System.Linq;
using Tests.Shared;
using Xunit;

namespace Tests.Unit
{
    public class NotificationHandlerServiceTests
    {
        private readonly NotificationHandlerService _notificationHandlerService;

        public NotificationHandlerServiceTests()
        {
            _notificationHandlerService = new NotificationHandlerService();
        }

        [Fact]
        public void DeveAdicionarUmaNotivicationCorretamente()
        {
            var notificationBuilder = new NotificationHandlerBuilder()
                .ComValue("Cliente Inválido")
                .Instanciar();

            _notificationHandlerService.AddDomainNotification(notificationBuilder);

            _notificationHandlerService.GetNotifications().Should().NotBeNull();
            _notificationHandlerService.GetNotifications().Should().HaveCount(1);
            _notificationHandlerService.GetNotifications().Should().BeEquivalentTo(notificationBuilder);
        }

        [Fact]
        public void DeveAdicionarMaisDeUmaNotivication()
        {
            var notificationBuilder = new NotificationHandlerBuilder()
                .ComValue("Cliente Inválido")
                .Instanciar();

            var notificationBuilder2 = new NotificationHandlerBuilder()
                .ComKey("Nome")
                .ComValue("Nome inválido")
                .Instanciar();

            var notificationBuilder3 = new NotificationHandlerBuilder()
                .ComKey("SobreNome")
                .ComValue("Sobre nome inválido")
                .Instanciar();

            _notificationHandlerService.AddDomainNotification(notificationBuilder);
            _notificationHandlerService.AddDomainNotification(notificationBuilder2);
            _notificationHandlerService.AddDomainNotification(notificationBuilder3);

            var resultado = _notificationHandlerService.GetNotifications().ToList();

            resultado.Should().NotBeNull();
            resultado.Should().HaveCount(3);
            resultado[0].Should().BeEquivalentTo(notificationBuilder);
            resultado[1].Should().BeEquivalentTo(notificationBuilder2);
            resultado[2].Should().BeEquivalentTo(notificationBuilder3);
        }

        [Fact]
        public void DeveRemoverUmaNotivicationCorretamente()
        {
            var notificationBuilder = new NotificationHandlerBuilder()
                .ComValue("Cliente Inválido")
                .Instanciar();

            _notificationHandlerService.AddDomainNotification(notificationBuilder);
            _notificationHandlerService.RemoveNotification(notificationBuilder);

            _notificationHandlerService.GetNotifications().Should().NotBeNull();
            _notificationHandlerService.GetNotifications().Should().HaveCount(0);
            _notificationHandlerService.GetNotifications().FirstOrDefault().Should().BeNull();
        }

        [Fact]
        public void DeveRemoverApenasANotivicationCorrespondenteNaLista()
        {
            var notificationBuilder = new NotificationHandlerBuilder()
                .ComValue("Cliente Inválido")
                .Instanciar();

            var notificationBuilder2 = new NotificationHandlerBuilder()
                .ComKey("Nome")
                .ComValue("Nome inválido")
                .Instanciar();

            var notificationBuilder3 = new NotificationHandlerBuilder()
                .ComKey("SobreNome")
                .ComValue("Sobre nome inválido")
                .Instanciar();

            _notificationHandlerService.AddDomainNotification(notificationBuilder);
            _notificationHandlerService.AddDomainNotification(notificationBuilder2);
            _notificationHandlerService.AddDomainNotification(notificationBuilder3);

            _notificationHandlerService.RemoveNotification(notificationBuilder2);

            var resultado = _notificationHandlerService.GetNotifications().ToList();

            resultado.Should().NotBeNull();
            resultado.Should().HaveCount(2);
            resultado[0].Should().BeEquivalentTo(notificationBuilder);
            resultado[1].Should().BeEquivalentTo(notificationBuilder3);
        }

        [Fact]
        public void NaoDeveAdicionarUmaNotificationNula()
        {
            _notificationHandlerService.AddDomainNotification(null);

            var resultado = _notificationHandlerService.GetNotifications().ToList();

            resultado.Should().BeEmpty();
            resultado.Should().NotBeNull();
            resultado.Should().HaveCount(0);
        }

        [Fact]
        public void NaoDeveAdicionarUmaNotificationExistente()
        {
            _notificationHandlerService.AddDomainNotification(new NotificationHandlerBuilder()
                .ComValue("Cliente Inválido")
                .Instanciar());

            _notificationHandlerService.AddDomainNotification(new NotificationHandlerBuilder()
                .ComValue("Cliente Inválido")
                .Instanciar());

            var resultado = _notificationHandlerService.GetNotifications().ToList();

            resultado.Should().NotBeEmpty();
            resultado.Should().NotBeNull();
            resultado.Should().HaveCount(1);
        }

        [Fact]
        public void NaoDeveRemoverUmaNotificationNula()
        {
            _notificationHandlerService.AddDomainNotification(new NotificationHandlerBuilder()
                .ComValue("Cliente Inválido")
                .Instanciar());

            _notificationHandlerService.RemoveNotification(null);
            var resultado = _notificationHandlerService.GetNotifications().ToList();

            resultado.Should().NotBeEmpty();
            resultado.Should().NotBeNull();
            resultado.Should().HaveCount(1);
        }

        [Fact]
        public void NaoDeveRemoverUmaNotificationJaRemovida()
        {
            var builder1 = new NotificationHandlerBuilder()
                .ComValue("Cliente Inválido")
                .Instanciar();

            var builder2 = new NotificationHandlerBuilder()
                .ComKey("Nome")
                .ComValue("Nome inválido")
                .Instanciar();

            _notificationHandlerService.AddDomainNotification(builder1);
            _notificationHandlerService.AddDomainNotification(builder2);

            _notificationHandlerService.GetNotifications().Should().HaveCount(2);

            _notificationHandlerService.RemoveNotification(builder1);
            _notificationHandlerService.RemoveNotification(builder1);

            var resultado = _notificationHandlerService.GetNotifications().ToList();

            resultado.Should().NotBeEmpty();
            resultado.Should().NotBeNull();
            resultado.Should().HaveCount(1);
            resultado[0].Should().BeEquivalentTo(builder2);
        }

        [Fact]
        public void DeveSerVerdadeiroQuandoHouverNotifications()
        {
            var builder1 = new NotificationHandlerBuilder()
                .ComValue("Cliente Inválido")
                .Instanciar();

            _notificationHandlerService.AddDomainNotification(builder1);

            _notificationHandlerService.HasNotifications().Should().BeTrue();
            _notificationHandlerService.GetNotifications().Should().HaveCount(1);
        }

        [Fact]
        public void DeveSerFalsoQuandoNaoHouverNotifications()
        {
            _notificationHandlerService.HasNotifications().Should().BeFalse();
            _notificationHandlerService.GetNotifications().Should().HaveCount(0);
        }

        [Fact]
        public void DeveSerVerdadeiroQuandoHouverMaisDeUmaNotification()
        {
            var builder1 = new NotificationHandlerBuilder()
                .ComValue("Cliente Inválido")
                .Instanciar();

            var builder2 = new NotificationHandlerBuilder()
                .ComKey("Nome")
                .ComValue("Nome inválido")
                .Instanciar();

            _notificationHandlerService.AddDomainNotification(builder1);
            _notificationHandlerService.AddDomainNotification(builder2);

            _notificationHandlerService.HasNotifications().Should().BeTrue();
            _notificationHandlerService.GetNotifications().Should().HaveCount(2);
        }

        [Fact]
        public void DeveSerVerdadeiroQuandoHouverMaisDeUmaNotificationEUmaDelasForRemovida()
        {
            var builder1 = new NotificationHandlerBuilder()
                .ComValue("Cliente Inválido")
                .Instanciar();

            var builder2 = new NotificationHandlerBuilder()
                .ComKey("Nome")
                .ComValue("Nome inválido")
                .Instanciar();

            _notificationHandlerService.AddDomainNotification(builder1);
            _notificationHandlerService.AddDomainNotification(builder2);

            _notificationHandlerService.RemoveNotification(builder1);

            _notificationHandlerService.HasNotifications().Should().BeTrue();
            _notificationHandlerService.GetNotifications().Should().HaveCount(1);
        }

        [Fact]
        public void DeveSerFalsoQuandoHouverMaisDeUmaNotificationETodasElasForemRemovida()
        {
            var builder1 = new NotificationHandlerBuilder()
                .ComValue("Cliente Inválido")
                .Instanciar();

            var builder2 = new NotificationHandlerBuilder()
                .ComKey("Nome")
                .ComValue("Nome inválido")
                .Instanciar();

            _notificationHandlerService.AddDomainNotification(builder1);
            _notificationHandlerService.AddDomainNotification(builder2);

            _notificationHandlerService.RemoveNotification(builder1);
            _notificationHandlerService.RemoveNotification(builder2);

            _notificationHandlerService.HasNotifications().Should().BeFalse();
            _notificationHandlerService.GetNotifications().Should().HaveCount(0);
        }
    }
}
