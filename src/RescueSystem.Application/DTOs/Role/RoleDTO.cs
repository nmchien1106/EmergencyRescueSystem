using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.DTOs.Role
{
    public class RoleDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

    }
}
