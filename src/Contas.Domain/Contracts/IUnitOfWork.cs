using System;

namespace Contas.Domain.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        bool Commit();
    } 
}