using Contas.Domain.Core.Events;

namespace Contas.Domain.Contracts
{
    public interface IEventStore
    {
        void SalvarEvento<T>(T evento) where T : Event;
    }
}