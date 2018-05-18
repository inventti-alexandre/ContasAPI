using System;
using System.Collections.Generic;
using AutoMapper;
using Contas.Application.Contracts;
using Contas.Application.ViewModels;
using Contas.Domain.Contas.Commands;
using Contas.Domain.Contas.Repository;
using Contas.Domain.Core.Bus;

namespace Contas.Application.Services
{
    public class ContaAppService : IContaAppService
    {
        private readonly IBus _bus;
        private readonly IMapper _mapper;
        private readonly IContaRepository _contaRepository;

        public ContaAppService(IBus bus, IMapper mapper, IContaRepository contaRepository)
        {
            _bus = bus;
            _mapper = mapper;
            _contaRepository = contaRepository;
        }

        public void Registrar(ContaViewModel contaViewModel)
        {
            var registroCmd = _mapper.Map<RegistrarContaCommand>(contaViewModel);
            _bus.SendCommand(registroCmd);
        }

        public void Atualizar(ContaViewModel contaViewModel)
        {
            var atualizarCmd = _mapper.Map<AtualizarContaCommand>(contaViewModel);
            _bus.SendCommand(atualizarCmd);
        }

        public void Desativar(Guid id)
        {
            _bus.SendCommand(new DesativarContaCommand(id));
        }

        public IEnumerable<ContaViewModel> ObterTodos()
        {
            return _mapper.Map<IEnumerable<ContaViewModel>>(_contaRepository.ObterTodos());
        }

        public IEnumerable<ContaViewModel> ObterContasPorUsuario(Guid usuario)
        {
            return _mapper.Map<IEnumerable<ContaViewModel>>(_contaRepository.ObterContasPorUsuario(usuario));
        }

        public ContaViewModel ObterPorId(Guid id)
        {
            return _mapper.Map<ContaViewModel>(_contaRepository.ObterPorId(id));
        }

        public void Dispose()
        {
            _contaRepository.Dispose();
        }
    }
}