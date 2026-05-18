using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.DTOs.Address
{
    public class AddressDTO
    {
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string GPS { get; set; }
    }
}
