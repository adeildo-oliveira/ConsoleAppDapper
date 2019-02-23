using ConsoleApp.Domain;
using ConsoleApp.InfraData;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tests.Integration.Tools;
using Tests.Shared;
using Xunit;

namespace Tests.Integration
{
    public class ClienteTests : IClassFixture<IntegrationTestFixture>
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteTests(IntegrationTestFixture fixture) => _clienteRepository = fixture.GetService<IClienteRepository>();

        [Fact]
        public async Task DeveRetornarUmClientePeloId()
        {
            var clienteBuilder = new ClienteBuilder()
                .ComId(Guid.NewGuid())
                .ComNome("Petit")
                .ComSobreNome("Gateau")
                .Criar();

            var resultado = await _clienteRepository.ObterCliente(clienteBuilder.Id);

            resultado.Should().NotBeNull();
            resultado.Should().BeEquivalentTo(clienteBuilder);
        }

        [Fact]
        public async Task DeveRetornarUmClienteComOIdCorrespondente()
        {
            var clienteBuilder = new ClienteBuilder()
                .ComId(Guid.NewGuid())
                .ComNome("Petit")
                .ComSobreNome("Gateau")
                .Criar();

            new ClienteBuilder()
                .ComId(Guid.NewGuid())
                .ComNome("Petit")
                .ComSobreNome("Gateau")
                .Criar();

            var resultado = await _clienteRepository.ObterCliente(clienteBuilder.Id);

            resultado.Should().NotBeNull();
            resultado.Should().BeEquivalentTo(clienteBuilder);
        }

        [Fact]
        public async Task DeveInserirClienteCorretamente()
        {
            var clienteBuilder = new ClienteBuilder()
                .ComId(Guid.NewGuid())
                .ComNome("Petit")
                .ComSobreNome("Gateau")
                .Instanciar();

            await _clienteRepository.InserirCliente(clienteBuilder);
            var resultado = await _clienteRepository.ObterCliente(clienteBuilder.Id);

            resultado.Should().NotBeNull();
            resultado.Should().BeEquivalentTo(clienteBuilder);
        }

        [Fact]
        public async Task DeveBuscarTodosOsClienteComEnderecoETelefone()
        {
            var clienteBuilder = new ClienteBuilder()
                .ComId(Guid.NewGuid())
                .ComNome("Petit")
                .ComSobreNome("Gateau")
                .Criar();

            var enderecoBuilder = new EnderecoBuilder()
                .ComId(Guid.NewGuid())
                .ComLogradouro("Av. 23 de Maio")
                .ComBairro("Liberdade")
                .ComCidade("São Paulo")
                .ComEstado("SP")
                .ComCliente(clienteBuilder)
                .Criar();

            var telefoneBuilder = new TelefoneBuilder()
                .ComId(Guid.NewGuid())
                .ComNumero("123456789")
                .ComCliente(clienteBuilder)
                .Criar();

            clienteBuilder.AdicionarEndereco(enderecoBuilder);
            clienteBuilder.AdicionarTelefone(telefoneBuilder);

            var resultado = (await _clienteRepository.ObterClientes()) as List<Cliente>;
            resultado.Should().HaveCount(1);
            resultado.Should().BeEquivalentTo(clienteBuilder);
            resultado[0].Enderecos.Should().BeEquivalentTo(enderecoBuilder);
            resultado[0].Telefones.Should().BeEquivalentTo(telefoneBuilder);
            resultado[0].Enderecos.Should().HaveCount(1);
            resultado[0].Telefones.Should().HaveCount(1);
        }

        [Fact]
        public async Task DeveBuscarTodosOsClienteComMaisDeUmEnderecoEUmTelefone()
        {
            var clienteBuilder = new ClienteBuilder()
                .ComId(Guid.NewGuid())
                .ComNome("Petit")
                .ComSobreNome("Gateau")
                .Criar();

            var enderecoBuilder = new EnderecoBuilder()
                .ComId(Guid.NewGuid())
                .ComLogradouro("Av. 23 de Maio")
                .ComBairro("Liberdade")
                .ComCidade("São Paulo")
                .ComEstado("SP")
                .ComCliente(clienteBuilder)
                .Criar();

            var enderecoBuilder2 = new EnderecoBuilder()
                .ComId(Guid.NewGuid())
                .ComLogradouro("Av. Angélica")
                .ComBairro("Consolação")
                .ComCidade("São Paulo")
                .ComEstado("SP")
                .ComCliente(clienteBuilder)
                .Criar();

            var enderecoBuilder3 = new EnderecoBuilder()
                .ComId(Guid.NewGuid())
                .ComLogradouro("Av. Angélica")
                .ComBairro("Pacaembu")
                .ComCidade("São Paulo")
                .ComEstado("SP")
                .ComCliente(clienteBuilder)
                .Criar();

            var telefoneBuilder = new TelefoneBuilder()
                .ComId(Guid.NewGuid())
                .ComNumero("123456789")
                .ComCliente(clienteBuilder)
                .Criar();

            clienteBuilder.AdicionarEndereco(enderecoBuilder);
            clienteBuilder.AdicionarEndereco(enderecoBuilder2);
            clienteBuilder.AdicionarEndereco(enderecoBuilder3);
            clienteBuilder.AdicionarTelefone(telefoneBuilder);

            var resultado = (await _clienteRepository.ObterClientes()).ToList();

            resultado.Should().HaveCount(1);
            resultado.Should().BeEquivalentTo(clienteBuilder);
            resultado[0].Enderecos.FirstOrDefault(x => x.Id == enderecoBuilder.Id).Should().BeEquivalentTo(enderecoBuilder);
            resultado[0].Enderecos.FirstOrDefault(x => x.Id == enderecoBuilder2.Id).Should().BeEquivalentTo(enderecoBuilder2);
            resultado[0].Enderecos.FirstOrDefault(x => x.Id == enderecoBuilder3.Id).Should().BeEquivalentTo(enderecoBuilder3);
            resultado[0].Telefones.Should().BeEquivalentTo(telefoneBuilder);
            resultado[0].Enderecos.Should().HaveCount(3);
            resultado[0].Telefones.Should().HaveCount(1);
        }

        [Fact]
        public async Task DeveBuscarTodosOsClienteComUmEnderecoEMaisDeUmTelefone()
        {
            var clienteBuilder = new ClienteBuilder()
                .ComId(Guid.NewGuid())
                .ComNome("Petit")
                .ComSobreNome("Gateau")
                .Criar();

            var enderecoBuilder = new EnderecoBuilder()
                .ComId(Guid.NewGuid())
                .ComLogradouro("Av. 23 de Maio")
                .ComBairro("Liberdade")
                .ComCidade("São Paulo")
                .ComEstado("SP")
                .ComCliente(clienteBuilder)
                .Criar();

            var telefoneBuilder = new TelefoneBuilder()
                .ComId(Guid.NewGuid())
                .ComNumero("123456789")
                .ComCliente(clienteBuilder)
                .Criar();

            var telefoneBuilder2 = new TelefoneBuilder()
                .ComId(Guid.NewGuid())
                .ComNumero("987654321")
                .ComCliente(clienteBuilder)
                .Criar();

            var telefoneBuilder3 = new TelefoneBuilder()
                .ComId(Guid.NewGuid())
                .ComNumero("147852369")
                .ComCliente(clienteBuilder)
                .Criar();

            clienteBuilder.AdicionarEndereco(enderecoBuilder);
            clienteBuilder.AdicionarTelefone(telefoneBuilder);

            var resultado = (await _clienteRepository.ObterClientes()).ToList();

            resultado.Should().HaveCount(1);
            resultado.Should().BeEquivalentTo(clienteBuilder);
            resultado[0].Enderecos.FirstOrDefault(x => x.Id == enderecoBuilder.Id).Should().BeEquivalentTo(enderecoBuilder);
            resultado[0].Telefones.FirstOrDefault(x => x.Id == telefoneBuilder.Id).Should().BeEquivalentTo(telefoneBuilder);
            resultado[0].Telefones.FirstOrDefault(x => x.Id == telefoneBuilder2.Id).Should().BeEquivalentTo(telefoneBuilder2);
            resultado[0].Telefones.FirstOrDefault(x => x.Id == telefoneBuilder3.Id).Should().BeEquivalentTo(telefoneBuilder3);
            resultado[0].Enderecos.Should().HaveCount(1);
            resultado[0].Telefones.Should().HaveCount(3);
        }
    }
}
