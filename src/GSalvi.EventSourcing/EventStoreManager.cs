using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GSalvi.EventSourcing
{
    internal class EventStoreManager<T> : IEventStoreManager<T> where T : Snapshot
    {
        private readonly ISnapshotRepository<T> _repository;
        private readonly ISnapshotBuilder<T> _builder;
        private readonly IEventSerializer _serializer;

        public EventStoreManager(
            ISnapshotRepository<T> repository,
            ISnapshotBuilder<T> builder,
            IEventSerializer serializer)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _builder = builder ?? throw new ArgumentNullException(nameof(builder));
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        public async Task StoreAsync<TR>(TR @event, Guid aggregateId) where TR : class
        {
            var id = Guid.NewGuid();
            var eventType = typeof(TR).Name;
            var serializedData = _serializer.Serialize(@event);
            var timestamp = DateTime.UtcNow;

            var snapshot = _builder.Create(id, aggregateId, eventType, serializedData, timestamp);

            await _repository.AddAsync(snapshot).ConfigureAwait(false);
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            return _repository.GetByIdAsync(id);
        }

        public Task<IEnumerable<T>> GetByAggregateIdAsync(Guid aggregateId)
        {
            return _repository.GetByAggregateIdAsync(aggregateId);
        }

        public Task<IEnumerable<T>> GetByEventTypeAsync(string eventType)
        {
            return _repository.GetByEventTypeAsync(eventType);
        }
    }
}