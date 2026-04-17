using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.DTOs.User
{
    // Server trả thông tin user về cho client
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Avatar { get; set; } = string.Empty;
        public List<string > Roles { get; set; } = new();

    }
}
