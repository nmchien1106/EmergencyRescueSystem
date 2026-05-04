using MediatR;
using RescueSystem.Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.Features.Auth.Commands.ForgotPassword
{
    public class ForgotPasswordCommand : IRequest<string>
    {
        public string Email { get; set; } = string.Empty;
    }
}
