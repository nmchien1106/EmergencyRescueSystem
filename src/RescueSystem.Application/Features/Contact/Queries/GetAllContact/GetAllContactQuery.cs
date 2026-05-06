using MediatR;
using RescueSystem.Application.DTOs.Contact;
using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.Features.Contact.Queries.GetAllContact
{
    public class GetAllContactQuery : IRequest<List<ContactDTO>>
    {
        public Guid UserId { get; set; }
    }
}
