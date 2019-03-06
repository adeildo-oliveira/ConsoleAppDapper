using ConsoleApp.Domain.ModelEntity;
using FluentValidation;
using System;

namespace ConsoleApp.Domain.Models
{
    public class Telefone : Entity<Telefone>
    {
        public Telefone(Guid id, string numero)
        {
            Id = id;
            Numero = numero;
        }

        public string Numero { get; private set; }

        public override bool EValido()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty).WithMessage("Id telefone inválido");

            RuleFor(c => c.Numero)
                .NotEmpty().When(m => string.IsNullOrWhiteSpace(m.Numero)).WithMessage("Número de telefone inválido");

            ValidationResult = Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
