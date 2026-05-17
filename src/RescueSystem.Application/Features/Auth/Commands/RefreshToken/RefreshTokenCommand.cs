using MediatR;
using RescueSystem.Application.DTOs.Auth;

namespace RescueSystem.Application.Features.Auth.Commands.RefreshToken;

public class RefreshTokenCommand : IRequest<AuthResponse>
{
    public string RefreshToken { get; set; } = string.Empty;
}