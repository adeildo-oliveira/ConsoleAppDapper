using ConsoleApp.Domain.ModelEntity;
using System;

namespace ConsoleApp.Domain.Models
{
    public class Telefone : Entity
    {
        public Telefone(Guid id, string numero)
        {
            Id = id;
            Numero = numero;
        }

        public string Numero { get; private set; }
    }
}
