using System;
using MediatR;
using System.Text.Json.Serialization;

namespace RescueSystem.Application.Features.User.Commands;

public class UpdateUserCommand : IRequest<bool>
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public string? FullName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Avatar { get; set; }
    public List<string>? Roles { get; set; }
}
