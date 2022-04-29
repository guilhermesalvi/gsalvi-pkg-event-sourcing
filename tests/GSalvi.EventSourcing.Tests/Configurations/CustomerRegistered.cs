using System;

namespace GSalvi.EventSourcing.Tests.Configurations;

public class CustomerRegistered
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}