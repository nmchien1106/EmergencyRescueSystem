using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.DTOs.Dispatcher
{
    public class DispatcherDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
    }
}
