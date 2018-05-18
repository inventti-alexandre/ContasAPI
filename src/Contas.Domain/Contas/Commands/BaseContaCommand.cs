using Contas.Domain.Core.Commands;
using System;

namespace Contas.Domain.Contas.Commands
{
    public abstract class BaseContaCommand : Command
    {
        public Guid Id { get; protected set; }
        public string Nome { get; protected set; }
        public DateTime Data { get; protected set; }
        public decimal Valor { get; protected set; }
        public bool Parcelado { get; protected set; }
        public int NumeroParcelas { get; protected set; }
        public string Observacao { get; protected set; }
        public Guid IdCategoria { get; protected set; }
        public Guid IdUsuario { get; protected set; }
    }
}