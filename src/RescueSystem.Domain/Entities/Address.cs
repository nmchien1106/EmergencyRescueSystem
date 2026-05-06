using RescueSystem.Domain.Entities.Base;

namespace RescueSystem.Domain.Entities
{
    public class Address : BaseEntities
    {
        public Guid UserId { get; set; }
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string GPS { get; set; } = string.Empty;

        // Navigation Properties
        public ApplicationUser? User { get; set; }
    }
}
