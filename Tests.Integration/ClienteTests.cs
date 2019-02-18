using ConsoleApp.Domain;
using ConsoleApp.InfraData;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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
            var resultado = (await _clienteRepository.ObtemClientes()) as List<Cliente>;
            resultado.Should().HaveCount(2);
            resultado[0].Enderecos.Should().HaveCount(1);
            resultado[0].Telefones.Should().HaveCount(3);

            resultado[1].Enderecos.Should().HaveCount(1);
            resultado[1].Telefones.Should().HaveCount(1);
        }
    }
}
