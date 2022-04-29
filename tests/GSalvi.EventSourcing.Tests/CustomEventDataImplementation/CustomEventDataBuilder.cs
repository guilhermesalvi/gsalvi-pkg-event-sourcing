using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GSalvi.EventSourcing.Tests.CustomEventDataImplementation;

public class CustomEventDataBuilder : IEventDataBuilder<CustomEventData>
{
    public Task<CustomEventData> BuildAsync(Guid aggregateId, string eventType, dynamic entity,
        params KeyValuePair<string, string>[] additionalParams)
    {
        var id = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;
        var userId = Guid.NewGuid();

        return Task.FromResult(new CustomEventData
        {
            Id = id,
            AggregateId = aggregateId,
            Entity = entity,
            EventType = eventType,
            Timestamp = timestamp,
            UserId = userId
        });
    }
}