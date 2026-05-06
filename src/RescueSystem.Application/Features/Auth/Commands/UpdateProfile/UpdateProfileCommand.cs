using MediatR;
using RescueSystem.Application.DTOs.Address;
using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.Features.Auth.Commands.UpdateProfile
{
    public class UpdateProfileCommand : IRequest<string>
    {
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; }
    }
}
