using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.DTOs.RescueTeam
{
    public class RescueTeamDTO
    {
        public Guid Id { get; set; }
        public string? TeamName { get; set; }
        public string? Status { get; set; }
    }
}
