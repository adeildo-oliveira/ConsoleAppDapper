using FluentAssertions;
using System;
using System.Linq;
using Tests.Shared;
using Xunit;

namespace Tests.Unit
{
    public class ClienteTests
    {
        [Fact]
        public void DeveAdicionarUmClienteCorretamente()
        {
            var cliente = new ClienteBuilder().ComNome("João").ComSobreNome("da Silva").Instanciar();
            cliente.Nome.Should().Be("João");
            cliente.SobreNome.Should().Be("da Silva");
        }

        [Fact]
        public void DeveAdicionarUmClienteComEndereco()
        {
            var cliente = new ClienteBuilder().ComNome("João").ComSobreNome("da Silva").Instanciar();
            var endereco = new EnderecoBuilder()
                .ComLogradouro("Av. Um")
                .ComBairro("São José")
                .ComCidade("São Paulo")
                .ComEstado("SP").Instanciar();

            cliente.Enderecos.Add(endereco);

            cliente.Nome.Should().Be("João");
            cliente.SobreNome.Should().Be("da Silva");
            cliente.Enderecos.Count().Should().Be(1);
            cliente.Enderecos.Should().BeEquivalentTo(endereco);
        }

        [Fact]
        public void NaoDeveAdicionarUmClienteComEnderecoDuplicado()
        {
            var id = Guid.NewGuid();
            var cliente = new ClienteBuilder().ComNome("João").ComSobreNome("da Silva").Instanciar();
            var endereco = new EnderecoBuilder()
                .ComId(id)
                .ComLogradouro("Av. Um")
                .ComBairro("São José")
                .ComCidade("São Paulo")
                .ComEstado("SP").Instanciar();

            cliente.AdicionarEndereco(endereco);
            cliente.AdicionarEndereco(endereco);

            cliente.Nome.Should().Be("João");
            cliente.SobreNome.Should().Be("da Silva");
            cliente.Enderecos.Count().Should().Be(1);
            cliente.Enderecos.Should().BeEquivalentTo(endereco);
        }

        [Fact]
        public void DeveAdicionarMaisDeUmEnderecoDoCliente()
        {
            var cliente = new ClienteBuilder().ComNome("João").ComSobreNome("da Silva").Instanciar();

            var endereco = new EnderecoBuilder()
                .ComId(Guid.NewGuid())
                .ComLogradouro("Av. Um")
                .ComBairro("São José")
                .ComCidade("São Paulo")
                .ComEstado("SP").Instanciar();

            var endereco2 = new EnderecoBuilder()
                .ComId(Guid.NewGuid())
                .ComLogradouro("Av. Um")
                .ComBairro("São José")
                .ComCidade("São Paulo")
                .ComEstado("SP").Instanciar();

            cliente.AdicionarEndereco(endereco);
            cliente.AdicionarEndereco(endereco2);

            cliente.Enderecos.Count().Should().Be(2);
            cliente.Enderecos.FirstOrDefault().Should().BeEquivalentTo(endereco);
            cliente.Enderecos.LastOrDefault().Should().BeEquivalentTo(endereco2);
        }

        [Fact]
        public void DeveAdicionarUmClienteComTelefone()
        {
            var cliente = new ClienteBuilder().ComNome("João").ComSobreNome("da Silva").Instanciar();
            var telefone = new TelefoneBuilder().ComNumero("(11) 25896-3698").Instanciar();

            cliente.AdicionarTelefone(telefone);

            cliente.Nome.Should().Be("João");
            cliente.SobreNome.Should().Be("da Silva");
            cliente.Telefones.Count().Should().Be(1);
            cliente.Telefones.Should().BeEquivalentTo(telefone);
        }

        [Fact]
        public void NaoDeveAdicionarUmClienteComTelefoneDuplicado()
        {
            var id = Guid.NewGuid();
            var cliente = new ClienteBuilder().ComNome("João").ComSobreNome("da Silva").Instanciar();
            var telefone = new TelefoneBuilder().ComId(id).ComNumero("(11) 25896-3698").Instanciar();

            cliente.AdicionarTelefone(telefone);
            cliente.AdicionarTelefone(telefone);

            cliente.Nome.Should().Be("João");
            cliente.SobreNome.Should().Be("da Silva");
            cliente.Telefones.Count().Should().Be(1);
            cliente.Telefones.Should().BeEquivalentTo(telefone);
        }

        [Fact]
        public void DeveAdicionarMaisDeUmTelefoneDoCliente()
        {
            var cliente = new ClienteBuilder().ComNome("João").ComSobreNome("da Silva").Instanciar();
            var telefone = new TelefoneBuilder().ComId(Guid.NewGuid()).ComNumero("(11) 25896-3698").Instanciar();
            var telefone2 = new TelefoneBuilder().ComId(Guid.NewGuid()).ComNumero("(11) 25896-3698").Instanciar();

            cliente.AdicionarTelefone(telefone);
            cliente.AdicionarTelefone(telefone2);

            cliente.Nome.Should().Be("João");
            cliente.SobreNome.Should().Be("da Silva");
            cliente.Telefones.Count().Should().Be(2);
            cliente.Telefones.FirstOrDefault().Should().BeEquivalentTo(telefone);
            cliente.Telefones.LastOrDefault().Should().BeEquivalentTo(telefone2);
        }

        [Fact]
        public void DeveAdicionarUmClienteComTelefoneEEndereco()
        {
            var cliente = new ClienteBuilder().ComNome("João").ComSobreNome("da Silva").Instanciar();
            var telefone = new TelefoneBuilder().ComNumero("(11) 25896-3698").Instanciar();
            var endereco = new EnderecoBuilder()
                .ComLogradouro("Av. Um")
                .ComBairro("São José")
                .ComCidade("São Paulo")
                .ComEstado("SP").Instanciar();

            cliente.Enderecos.Add(endereco);
            cliente.Telefones.Add(telefone);

            cliente.Nome.Should().Be("João");
            cliente.SobreNome.Should().Be("da Silva");

            cliente.Telefones.Count().Should().Be(1);
            cliente.Telefones.Should().BeEquivalentTo(telefone);
            cliente.Enderecos.Count().Should().Be(1);
            cliente.Enderecos.Should().BeEquivalentTo(endereco);
        }
    }
}
