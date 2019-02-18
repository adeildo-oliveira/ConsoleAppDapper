using ConsoleApp.Domain;
using System;

namespace Tests.Shared
{
    public class TelefoneBuilder : InMemoryBuilder<Telefone>
    {
        private Guid _id;
        private string _numero;

        public TelefoneBuilder ComId(Guid id)
        {
            _id = id;
            return this;
        }

        public TelefoneBuilder ComNumero(string numero)
        {
            _numero = numero;
            return this;
        }

        public override Telefone Instanciar() => new Telefone(_id, _numero);
    }
}
