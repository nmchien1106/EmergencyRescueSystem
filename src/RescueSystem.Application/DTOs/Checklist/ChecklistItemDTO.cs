namespace RescueSystem.Application.Features.Checklist
{
    public class ChecklistItemDTO
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public bool IsCheck { get; set; }
    }
}