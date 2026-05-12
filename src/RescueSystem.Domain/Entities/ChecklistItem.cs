using RescueSystem.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Domain.Entities
{
    public class ChecklistItem : BaseEntities
    {
        public string Description { get; set; }
        public bool IsCheck { get; set; }
        public Guid ChecklistId { get; set; }
        // navigation
        public Checklist Checklist { get; set; }
    }
}
