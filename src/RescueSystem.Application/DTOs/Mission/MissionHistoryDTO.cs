using System;
using RescueSystem.Application.DTOs.Common;
using RescueSystem.Application.DTOs.User;
using RescueSystem.Domain.Enums;

namespace RescueSystem.Application.DTOs.Mission
{
    public class MissionHistoryDTO
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid MissionId { get; set; }
        public MissionStatus? FromStatus { get; set; }
        public MissionStatus ToStatus { get; set; }
        public Guid ChangedById { get; set; }
        public string Note { get; set; } = string.Empty;

        public UserDTO? ChangedBy { get; set; }
    }
}