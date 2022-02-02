using System;
using System.Diagnostics.CodeAnalysis;

namespace GSalvi.EventSourcing.Tests.Configurations.Models;

[ExcludeFromCodeCoverage]
public class CustomerRegistered
{
    public Guid CustomerId { get; set; }
    public string? CustomerName { get; set; }
}