using System;

namespace GSalvi.EventSourcing.Tests.CustomEventDataImplementation;

public class CustomEventData : EventData
{
    public Guid UserId { get; set; }
}