using System;
using Contas.Domain.Core.Events;

namespace Contas.Domain.Contas.Events
{
    public abstract class BaseContaEvent : Event
    {
        public Guid Id { get; protected set; }
        public string Nome { get; protected set; }
        public DateTime Data { get; protected set; }
        public decimal Valor { get; protected set; }
        public bool Parcelado { get; protected set; }
        public int NumeroParcelas { get; protected set; }
        public string Observacao { get; protected set; }
    }
}