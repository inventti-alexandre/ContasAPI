using System;

namespace Contas.Domain.Contas.Commands
{
    public class ExcluirContaCommand : BaseContaCommand
    {
        public ExcluirContaCommand(Guid id)
        {
            Id = id;
            AggregateId = Id;
        }
    }
}