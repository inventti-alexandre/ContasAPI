using System;
using Contas.Application.ViewModels;

namespace Contas.Application.Contracts
{
    public interface IUsuarioAppService : IDisposable
    {
        void Registrar(UsuarioViewModel usuarioViewModel);
    }
}