using Contas.Domain.Usuarios;
using Contas.Domain.Usuarios.Repository;
using Contas.Infrastructure.Data.Context;

namespace Contas.Infrastructure.Data.Repository
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(ContasContext context) : base(context)
        {
        }
    }
}