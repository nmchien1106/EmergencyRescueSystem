using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.DTOs.Request
{
    public class RequestDTO
    {
        public Guid Id { get; set; }
        public string? Description { get; set; }
        public string? EmergencyType { get; set; }
        public string? Priority { get; set; }
    }
}
