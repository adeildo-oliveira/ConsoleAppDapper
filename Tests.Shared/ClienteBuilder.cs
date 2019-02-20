using ConsoleApp.Domain;
using System;

namespace Tests.Shared
{
    public class ClienteBuilder : DatabaseBuilder<Cliente>
    {
        private Endereco _endereco;
        private Guid _id;
        private string _nome;
        private string _sobreNome;

        public ClienteBuilder ComId(Guid id)
        {
            _id = id;
            return this;
        }

        public ClienteBuilder ComNome(string nome)
        {
            _nome = nome;
            return this;
        }

        public ClienteBuilder ComSobreNome(string sobreNome)
        {
            _sobreNome = sobreNome;
            return this;
        }

        public ClienteBuilder ComEndereco(Endereco endereco)
        {
            _endereco = endereco;
            return this;
        }

        public override Cliente Instanciar()
        {
            var cliente = new Cliente(_id, _nome, _sobreNome);
            cliente.AdicionarEndereco(_endereco);
            return cliente;
        }
    }
}
