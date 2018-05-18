using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Contas.Domain.Contas.Events
{
    public class ContaEventHandler : INotificationHandler<ContaRegistradaEvent>, INotificationHandler<ContaAtualizadaEvent>, INotificationHandler<ContaExcluidaEvent>
    {
        public Task Handle(ContaRegistradaEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(ContaAtualizadaEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(ContaExcluidaEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}