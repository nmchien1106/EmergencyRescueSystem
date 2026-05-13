using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.DTOs.Checklist
{
    public class ChecklistDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid MissionId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
