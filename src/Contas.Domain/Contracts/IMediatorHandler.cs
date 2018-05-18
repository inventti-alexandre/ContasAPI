using System.Threading.Tasks;
using Contas.Domain.Core.Commands;
using Contas.Domain.Core.Events;

namespace Contas.Domain.Contracts
{
    public interface IMediatorHandler
    {
        Task PublicarEvento<T>(T evento) where T : Event;
        Task EnviarComando<T>(T comando) where T : Command;
    }
}