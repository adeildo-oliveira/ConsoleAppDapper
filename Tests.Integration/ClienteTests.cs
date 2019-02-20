using ConsoleApp.Domain;
using ConsoleApp.InfraData;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tests.Shared;
using Xunit;

namespace Tests.Integration
{
    public class ClienteTests
    {
        private readonly ClienteRepository _clienteRepository;

        public ClienteTests()
        {
            _clienteRepository = new ClienteRepository();
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
    }
}
