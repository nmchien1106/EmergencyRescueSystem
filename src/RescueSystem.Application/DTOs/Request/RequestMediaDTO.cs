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

        //TODO: Dieu 17/05/2026: Them transferUrl de client co the lay
        [System.Text.Json.Serialization.JsonPropertyName("mediaUrl")]
        public string SecureUrl { get; set; } = string.Empty;
        public string PublicId { get; set; } = string.Empty;
        public MediaType ResourceType { get; set; } = MediaType.Image;
    }
}
