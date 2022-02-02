using System;
using System.Diagnostics.CodeAnalysis;

namespace GSalvi.EventSourcing.Tests.Configurations.Models;

[ExcludeFromCodeCoverage]
public class CustomEventData : EventData
{
    public Guid UserId { get; set; }
    public string? UserName { get; set; }
    public string? UserEmail { get; set; }
}