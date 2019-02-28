using ConsoleApp.Domain.Models;
using System;

namespace Tests.Shared
{
    public class ClienteBuilder : DatabaseBuilder<Cliente>
    {
        private Endereco _endereco;
        private Telefone _telefone;
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

        public override Cliente Criar() => Criar($"insert into Cliente values ('{_id}', '{_nome}', '{_sobreNome}')");
    }
}
