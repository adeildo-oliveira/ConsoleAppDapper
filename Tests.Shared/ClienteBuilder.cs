using ConsoleApp.Domain;
using System;

namespace Tests.Shared
{
    public class ClienteBuilder : InMemoryBuilder<Cliente>
    {
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

        public override Cliente Instanciar() => new Cliente(_id, _nome, _sobreNome);
    }
}
