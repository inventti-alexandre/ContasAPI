using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Contas.Domain.Usuarios.Events
{
    public class UsuarioEventHandler : INotificationHandler<UsuarioRegistradoEvent>
    {
        public Task Handle(UsuarioRegistradoEvent message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}