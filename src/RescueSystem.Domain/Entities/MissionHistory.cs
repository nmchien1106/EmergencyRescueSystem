using System;
using RescueSystem.Domain.Entities.Base;
using RescueSystem.Domain.Enums;

namespace RescueSystem.Domain.Entities
{
    public class MissionHistory : BaseEntities
    {
        public Guid MissionId { get; set; }

        public MissionStatus? FromStatus { get; set; }

        public MissionStatus ToStatus { get; set; }

        public Guid ChangedById { get; set; }

        public string Note { get; set; } = string.Empty;

        // Navigation Properties
        public Mission? Mission { get; set; }
        public ApplicationUser? ChangedBy { get; set; }
    }
}
