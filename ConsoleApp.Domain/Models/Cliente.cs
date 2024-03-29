﻿using ConsoleApp.Domain.ModelEntity;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp.Domain.Models
{
    public class Cliente : Entity<Cliente>
    {
        public Cliente(Guid id, string nome, string sobreNome)
        {
            Id = id;
            Nome = nome;
            SobreNome = sobreNome;
            Enderecos = new List<Endereco>();
            Telefones = new List<Telefone>();
        }

        public string Nome { get; private set; }
        public string SobreNome { get; private set; }
        public ICollection<Endereco> Enderecos { get; private set; }
        public ICollection<Telefone> Telefones { get; private set; }

        public virtual void AdicionarEndereco(Endereco endereco)
        {
            if(!Enderecos.Any(x => x.Id == endereco.Id))
                Enderecos.Add(endereco);
        }

        public virtual void AdicionarTelefone(Telefone telefone)
        {
            if(!Telefones.Any(x => x.Id == telefone.Id))
                Telefones.Add(telefone);
        }

        public override bool EValido()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty).WithMessage("Id cliente inválido");

            RuleFor(c => c.Nome)
                .NotEmpty().When(m => string.IsNullOrWhiteSpace(m.Nome)).WithMessage("Nome inválido");
            RuleFor(c => c.SobreNome)
                .NotEmpty().When(m => string.IsNullOrWhiteSpace(m.SobreNome)).WithMessage("Sobre nome inválido");

            ValidationResult = Validate(this);
            return ValidationResult.IsValid;
        }

        public override bool Equals(object obj)
        {
            if (this == null)
                return false;

            if (obj == null)
                return false;

            return Id == ((Cliente)obj).Id;
        }

        public static bool operator ==(Cliente obj1, Cliente obj2)
        {
            if (ReferenceEquals(obj1, obj2))
                return true;

            if (ReferenceEquals(obj1, null))
                return false;

            if (ReferenceEquals(obj2, null))
                return false;

            return obj1.Id == obj2.Id;
        }

        public static bool operator !=(Cliente obj1, Cliente obj2)
        {
            if (ReferenceEquals(obj1, obj2))
                return false;

            if (ReferenceEquals(obj1, null))
                return true;

            if (ReferenceEquals(obj2, null))
                return true;

            return !(obj1.Id == obj2.Id);
        }

        public override int GetHashCode() => Id.GetHashCode();
    }
}
