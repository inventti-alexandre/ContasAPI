using System;
using Contas.Domain.Core.Models;
using FluentValidation;
using System.Collections.Generic;

namespace Contas.Domain.Contas
{
    public class Categoria : Entity<Categoria>
    {
        public string Nome { get; private set; }
        public string Descricao { get; private set; }

        // Propriedades do EntityFramework
        public virtual ICollection<Conta> Contas { get; set; }

        //Construtor para a factory
        private Categoria() {}

        public override bool IsValid()
        {
            RuleFor(c => c.Nome).NotEmpty().WithMessage("Informe o nome");
            RuleFor(c => c.Descricao).NotEmpty().WithMessage("Informe uma descrição");
            ValidationResult = Validate(this);
            return ValidationResult.IsValid;
        }

        public static class CategoriaFactory
        {
            public static Categoria NovaCategoria(Guid id, string nome, string descricao)
            {
                var categoria = new Categoria()
                {
                    Id = id,
                    Nome = nome,
                    Descricao = descricao
                };

                return categoria;
            }
        }
    }
}