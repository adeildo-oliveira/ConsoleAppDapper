using ConsoleApp.Domain;
using ConsoleApp.InfraData;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tests.Integration.Tools;
using Tests.Shared;
using Xunit;

namespace Tests.Integration
{
    public class ClienteTests : IClassFixture<IntegrationTestFixture>
    {
        private ServiceProvider _serviceProvide;
        private readonly IClienteRepository _clienteRepository;

        public ClienteTests(IntegrationTestFixture fixture)
        {
            _serviceProvide = fixture.ServiceProvider;
            _serviceProvide.GetServices<IClienteRepository>();
        }

        [Fact]
        public async Task DeveBuscarTodosCliente()
        {
            var id = Guid.NewGuid();
            var clienteBuilder = new ClienteBuilder()
                .ComId(id)
                .ComNome("Petit")
                .ComSobreNome("Oliveira")
                .Criar($"insert into Cliente values('{id}', 'Petit', 'Oliveira')");

            var enderecoBuilder = new EnderecoBuilder()
                .ComLogradouro("Av. Um")
                .ComBairro("São José")
                .ComCidade("São Paulo")
                .ComEstado("SP")
                .Criar($"insert into Endereco values('{Guid.NewGuid()}', '{id}', 'Av. Um', 'São José', 'São Paulo', 'SP')");

            var resultado = (await _clienteRepository.ObtemClientes()) as List<Cliente>;
            resultado.Should().HaveCount(1);
            resultado[0].Enderecos.Should().HaveCount(1);
            resultado[0].Telefones.Should().HaveCount(3);

            resultado[1].Enderecos.Should().HaveCount(1);
            resultado[1].Telefones.Should().HaveCount(1);
        }

        [Fact]
        public async Task DeveInserirCliente()
        {
            var id = Guid.NewGuid();
            var clienteBuilder = new ClienteBuilder()
                .ComId(id)
                .ComSobreNome("")
                .ComNome("Petit").Instanciar();

            await new AutoMocker().CreateInstance<ClienteRepository>().InserirCliente(clienteBuilder);
        }
    }
}
