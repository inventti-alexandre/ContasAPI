using Contas.Domain.Core.Models;
using Contas.Domain.Usuarios;
using FluentValidation;
using System;

namespace Contas.Domain.Contas
{
    public class Conta : Entity<Conta>
    {
        // Propriedades
        public string Nome { get; private set; }
        public DateTime Data { get; private set; }
        public decimal Valor { get; private set; }
        public bool Parcelado { get; private set; }
        public int NumeroParcelas { get; private set; }
        public string Observacao { get; private set; }
        public bool Desativado { get; private set; }
        public Guid IdCategoria { get; private set; }
        public Guid IdUsuario { get; private set; }

        // Propriedades do EntityFramework
        public virtual Categoria Categoria { get; private set; }
        public virtual Usuario Usuario { get; private set; }

        //Construtor para a factory
        private Conta() { }

        public void DesativarConta()
        {
            Desativado = true;
        }

        public override bool IsValid()
        {
            RuleFor(c => c.Nome).NotEmpty().WithMessage("Informe o nome").Length(2, 100).WithMessage("O nome da conta precisa ter entre 2 e 100 caracteres.");
            RuleFor(c => c.Valor).NotEmpty().WithMessage("Informe o valor").GreaterThan(0).WithMessage("O valor da conta precisa ser maior que zero.");
            RuleFor(c => c.Data).NotEmpty().WithMessage("Informe a data");
            RuleFor(c => c.NumeroParcelas).GreaterThan(0).When(c => c.Parcelado).WithMessage("Informe o número de parcelas");
            RuleFor(c => c.NumeroParcelas).Equal(0).When(c => !c.Parcelado).WithMessage("O número de parcelas deve ser zero para contas não parceladas");

            ValidationResult = Validate(this);
            return ValidationResult.IsValid;
        }

        public void AtribuirCategoria(Categoria categoria)
        {
            if (!categoria.IsValid()) return;
            Categoria = categoria;
        }

        public static class ContaFactory
        {
            public static Conta NovaConta(Guid id, string nome, DateTime data, decimal valor, bool parcelado, int numeroParcelas, string observacao, Guid idCategoria, Guid idUsuario)
            {
                var conta = new Conta()
                {
                    Id = id,
                    Nome = nome,
                    Data = data,
                    Valor = valor,
                    Parcelado = parcelado,
                    NumeroParcelas = numeroParcelas,
                    Observacao = observacao,
                    IdCategoria = idCategoria,
                    IdUsuario = idUsuario
                };

                return conta;
            }
        }
    }
}
