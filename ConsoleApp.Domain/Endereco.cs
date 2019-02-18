using ConsoleApp.Domain.ModelEntity;
using System;

namespace ConsoleApp.Domain
{
    public class Endereco : Entity
    {
        public Endereco(Guid id, string logradouro, string bairro, string cidade, string estado)
        {
            Id = id;
            Logradouro = logradouro;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
        }

        public string Logradouro { get; private set; }
        public string Bairro { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
    }
}
