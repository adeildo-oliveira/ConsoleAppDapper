using FluentValidation;
using FluentValidation.Results;
using System;

namespace ConsoleApp.Domain.ModelEntity
{
    public abstract class Entity<T> : AbstractValidator<T> where T : class
    {
        public Guid Id { get; protected set; } = Guid.NewGuid();
        public abstract bool EValido();
        public virtual ValidationResult ValidationResult { get; protected set; }

        protected Entity() => ValidationResult = ValidationResult ?? new ValidationResult();
    }
}
