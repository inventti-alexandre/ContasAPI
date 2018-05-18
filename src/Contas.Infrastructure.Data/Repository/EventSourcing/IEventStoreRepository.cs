using System;
using System.Collections.Generic;
using Contas.Domain.Core.Events;

namespace Contas.Infrastructure.Data.Repository.EventSourcing
{
    public interface IEventStoreRepository : IDisposable
    {
        void Store(StoredEvent theEvent);
        IList<StoredEvent> All(Guid aggregateId);
    }
}