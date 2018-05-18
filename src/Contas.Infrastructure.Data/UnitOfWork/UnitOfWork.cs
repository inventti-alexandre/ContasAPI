using Contas.Domain.Contracts;
using Contas.Infrastructure.Data.Context;

namespace Contas.Infrastructure.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ContasContext _context;

        public UnitOfWork(ContasContext context)
        {
            _context = context;
        }

        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}