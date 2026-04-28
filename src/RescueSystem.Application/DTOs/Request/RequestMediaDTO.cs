using System;
using System.Collections.Generic;
using System.Text;
using RescueSystem.Domain.Enums;

namespace RescueSystem.Application.DTOs.Request
{
    public class RequestMediaDTO
    {
        public Guid Id { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string SescueUrl { get; set; } = string.Empty;
        public string PublicId { get; set; } = string.Empty;
        public MediaType ResourceType { get; set; } = MediaType.Image;
    }
}
