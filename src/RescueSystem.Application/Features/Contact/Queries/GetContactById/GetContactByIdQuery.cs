using MediatR;
using RescueSystem.Application.DTOs.Contact;
using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.Features.Contact.Queries.GetContactById
{
    public class GetContactDetailQuery : IRequest<ContactDTO>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}
