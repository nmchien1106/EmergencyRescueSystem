using RescueSystem.Domain.Entities.Base;
using RescueSystem.Domain.Enums;

namespace RescueSystem.Domain.Entities
{
    public class Mission : BaseEntities
    {
        public Guid RequestId { get; set; }

        public Guid DispatcherId { get; set; }

        public Guid RescueTeamId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public MissionStatus Status { get; set; } = MissionStatus.ASSIGNED;

        // Navigation Properties
        public RescueRequest? Request { get; set; }
        public ApplicationUser? Dispatcher { get; set; }
        public RescueTeam? RescueTeam { get; set; }
        public ICollection<Report> Reports { get; set; } = new List<Report>();
    }
}