namespace RescueSystem.Application.Features.Checklist
{
    public class ChecklistDetailDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid MissionId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<ChecklistItemDTO> Items { get; set; }
    }
}