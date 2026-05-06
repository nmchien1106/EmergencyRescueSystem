using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.DTOs.Contact
{
    public class ContactDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Relationship { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
