using System;

namespace Contas.Domain.Contas.Events
{
    public class ContaExcluidaEvent : BaseContaEvent
    {
        public ContaExcluidaEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }
    }
}