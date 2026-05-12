using RescueSystem.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Domain.Entities
{
    public class Checklist : BaseEntities
    {
        public string Title { get; set; }
        public Guid MissionId { get; set; }

        // navigation
        public Mission Mission { get; set; }
        public ICollection<ChecklistItem> ChecklistItems { get; set; }
    }
}
