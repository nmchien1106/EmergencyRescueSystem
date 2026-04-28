using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.DTOs.Mission
{
    public class MissionDTO
    {
        public Guid Id { get; set; }
        public Guid RequestId { get; set; }
        public Guid DispatcherId { get; set; }
        public Guid RescueTeamId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Status { get; set; }
        public string? TeamName { get; set; }
    }
}
