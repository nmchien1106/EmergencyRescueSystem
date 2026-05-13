using RescueSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.Common.Interfaces.Repositories
{
    public interface IChecklistItemRepository
    {
        Task AddAsync(ChecklistItem item);
        Task<ChecklistItem?> GetByIdAsync(Guid id);
        void Update(ChecklistItem item);
        void Delete(ChecklistItem item);
        Task SaveChangesAsync(CancellationToken cancellationToken);
        Task<List<ChecklistItem>> GetByChecklistIdAsync(Guid checklistId);
    }
}
