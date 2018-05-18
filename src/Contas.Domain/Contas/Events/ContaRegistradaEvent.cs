using System;
using Contas.Domain.Core.Events;

namespace Contas.Domain.Contas.Events
{
    public class ContaRegistradaEvent : BaseContaEvent
    {
        public ContaRegistradaEvent(Guid id, string nome, DateTime data, decimal valor, bool parcelado, int numeroParcelas, string observacao)
        {
            Id = id;
            Nome = nome;
            Data = data;
            Valor = valor;
            Parcelado = parcelado;
            NumeroParcelas = numeroParcelas;
            Observacao = observacao;
            AggregateId = id;
        }
    }
}