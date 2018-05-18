using System;
using System.Collections.Generic;
using Contas.Application.ViewModels;

namespace Contas.Application.Contracts
{
    public interface IContaAppService : IDisposable
    {
        void Registrar(ContaViewModel contaViewModel);
        void Atualizar(ContaViewModel contaViewModel);
        void Desativar(Guid id);
        IEnumerable<ContaViewModel> ObterTodos();
        IEnumerable<ContaViewModel> ObterContasPorUsuario(Guid usuario);
        ContaViewModel ObterPorId(Guid id);
    }
}