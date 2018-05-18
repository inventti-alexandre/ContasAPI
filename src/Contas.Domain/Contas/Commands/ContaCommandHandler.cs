using Contas.Domain.Contas.Events;
using Contas.Domain.Contas.Repository;
using Contas.Domain.Contracts;
using Contas.Domain.Core.Notifications;
using Contas.Domain.Handlers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Contas.Domain.Contas.Commands
{
    public class ContaCommandHandler : CommandHandler, IRequestHandler<RegistrarContaCommand>, IRequestHandler<AtualizarContaCommand>, IRequestHandler<ExcluirContaCommand>
    {
        private readonly IContaRepository _contaRepository;
        private readonly IUser _user;
        private readonly IMediatorHandler _mediator;

        public ContaCommandHandler(IUnitOfWork uow, IMediatorHandler mediator, INotificationHandler<DomainNotification> notifications, IContaRepository contaRepository, IUser user) : base(uow, mediator, notifications)
        {
            _contaRepository = contaRepository;
            _user = user;
            _mediator = mediator;
        }

        public Task Handle(RegistrarContaCommand request, CancellationToken cancellationToken)
        {
            var conta = Conta.ContaFactory.NovaConta(request.Id, request.Nome, request.Data, request.Valor, request.Parcelado, request.NumeroParcelas, request.Observacao, request.IdCategoria, request.IdUsuario);

            if (!IsValid(conta)) return Task.CompletedTask;

            _contaRepository.Adicionar(conta);

            if (Commit())
            {
                _mediator.PublicarEvento(new ContaRegistradaEvent(conta.Id, conta.Nome, conta.Data, conta.Valor, conta.Parcelado, conta.NumeroParcelas, conta.Observacao));
            }

            return Task.CompletedTask;
        }

        public Task Handle(AtualizarContaCommand request, CancellationToken cancellationToken)
        {
            var contaExistente = _contaRepository.ObterPorId(request.Id);

            if (contaExistente == null)
            {
                _mediator.PublicarEvento(new DomainNotification(request.MessageType, "Esta conta não existe."));
                return Task.CompletedTask;
            }

            if (contaExistente.IdUsuario != _user.GetUserId())
            {
                _mediator.PublicarEvento(new DomainNotification(request.MessageType, "Conta não pertencente ao usuário"));
                return Task.CompletedTask;
            }

            var conta = Conta.ContaFactory.NovaConta(request.Id, request.Nome, request.Data, request.Valor, request.Parcelado, request.NumeroParcelas, request.Observacao, request.IdCategoria, request.IdUsuario);

            if (!IsValid(conta)) return Task.CompletedTask;

            _contaRepository.Atualizar(conta);

            if (Commit())
            {
                _mediator.PublicarEvento(new ContaAtualizadaEvent(conta.Id, conta.Nome, conta.Data, conta.Valor, conta.Parcelado, conta.NumeroParcelas, conta.Observacao));
            }

            return Task.CompletedTask;
        }

        public Task Handle(ExcluirContaCommand request, CancellationToken cancellationToken)
        {
            var contaExistente = _contaRepository.ObterPorId(request.Id);

            if (contaExistente == null)
            {
                _mediator.PublicarEvento(new DomainNotification(request.MessageType, "Esta conta não existe."));
                return Task.CompletedTask;
            }

            if (contaExistente.IdUsuario != _user.GetUserId())
            {
                _mediator.PublicarEvento(new DomainNotification(request.MessageType, "Conta não pertencente ao usuário"));
                return Task.CompletedTask;
            }

            contaExistente.DesativarConta();

            _contaRepository.Atualizar(contaExistente);

            if (Commit())
            {
                _mediator.PublicarEvento(new ContaExcluidaEvent(request.Id));
            }

            return Task.CompletedTask;
        }

        private bool IsValid(Conta conta)
        {
            if (conta.IsValid()) return true;
            NotificarErrosValidacao(conta.ValidationResult);
            return false;
        }
    }
}