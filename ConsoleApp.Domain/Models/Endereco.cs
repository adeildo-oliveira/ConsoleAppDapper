using ConsoleApp.Domain.ModelEntity;
using FluentValidation;
using System;

namespace ConsoleApp.Domain.Models
{
    public class Endereco : Entity<Endereco>
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

        public override bool EValido()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty).WithMessage("Id endereço inválido");

            RuleFor(c => c.Logradouro)
                .NotEmpty().When(m => string.IsNullOrWhiteSpace(m.Logradouro)).WithMessage("Endereço inválido");
            RuleFor(c => c.Bairro)
                .NotEmpty().When(m => string.IsNullOrWhiteSpace(m.Bairro)).WithMessage("Bairro inválido");
            RuleFor(c => c.Cidade)
                .NotEmpty().When(m => string.IsNullOrWhiteSpace(m.Cidade)).WithMessage("Cidade inválido");

            ValidationResult = Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
