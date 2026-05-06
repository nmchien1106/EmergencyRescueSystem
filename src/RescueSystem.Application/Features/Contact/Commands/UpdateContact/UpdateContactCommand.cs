using MediatR;
using RescueSystem.Application.DTOs.Contact;
using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.Features.Contact.Commands.UpdateContact
{
    public class UpdateContactCommand : IRequest<ContactDTO>
    {
        public Guid Id { get; set; } // contactId

        public string Name { get; set; } = string.Empty;
        public string Relationship { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public Guid UserId { get; set; } // server gán
    }
}
