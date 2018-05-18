using Contas.Domain.Contracts;
using Contas.Domain.Core.Notifications;
using Contas.Domain.Usuarios.Events;
using Contas.Domain.Usuarios.Repository;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Contas.Domain.Handlers;

namespace Contas.Domain.Usuarios.Commands
{
    public class UsuarioCommandHandler : CommandHandler, IRequestHandler<RegistrarUsuarioCommand>
    {
        private readonly IMediatorHandler _mediator;
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioCommandHandler(IUnitOfWork uow, INotificationHandler<DomainNotification> notifications, IUsuarioRepository usuarioRepository, IMediatorHandler mediator) : base(uow, mediator, notifications)
        {
            _usuarioRepository = usuarioRepository;
            _mediator = mediator;
        }

        public Task Handle(RegistrarUsuarioCommand request, CancellationToken cancellationToken)
        {
            var usuario = Usuario.UsuarioFactory.NovoUsuario(request.Id, request.Nome, request.Sobrenome, request.DataNascimento, request.CPF, request.Email);

            if (!usuario.IsValid())
            {
                NotificarErrosValidacao(usuario.ValidationResult);
                return Task.CompletedTask;
            }

            var usuarioExistente = _usuarioRepository.Buscar(o => o.CPF == usuario.CPF || o.Email == usuario.Email);

            if (usuarioExistente.Any())
            {
                _mediator.PublicarEvento(new DomainNotification(request.MessageType, "CPF ou e-mail já utilizados"));
            }

            _usuarioRepository.Adicionar(usuario);

            if (Commit())
            {
                _mediator.PublicarEvento(new UsuarioRegistradoEvent(usuario.Id, usuario.Nome, usuario.Sobrenome, usuario.CPF, usuario.Email, usuario.DataNascimento));
            }

            return Task.CompletedTask;
        }
    }
}