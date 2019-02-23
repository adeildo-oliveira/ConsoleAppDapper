using ConsoleApp.Domain;
using System;

namespace Tests.Shared
{
    public class TelefoneBuilder : DatabaseBuilder<Telefone>
    {
        private Guid _id;
        private string _numero;
        private Cliente _cliente;

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

        public TelefoneBuilder ComCliente(Cliente cliente)
        {
            _cliente = cliente;
            return this;
        }

        public override Telefone Instanciar() => new Telefone(_id, _numero);

        public override Telefone Criar() =>
            Criar($"insert into Telefone values ('{_id}', '{_cliente.Id}', '{_numero}')");
    }
}
