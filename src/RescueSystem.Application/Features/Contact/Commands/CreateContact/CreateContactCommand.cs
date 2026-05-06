using MediatR;
using RescueSystem.Application.DTOs.Contact;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace RescueSystem.Application.Features.Contact.Commands.CreateContact
{
    public class CreateContactCommand : IRequest<ContactDTO>
    {
        public string Name { get; set; } = string.Empty;
        public string Relationship { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        [JsonIgnore]
        public Guid UserId { get; set; }
    }
}
