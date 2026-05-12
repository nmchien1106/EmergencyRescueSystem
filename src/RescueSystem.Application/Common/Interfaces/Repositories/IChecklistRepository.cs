using RescueSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.Common.Interfaces.Repositories
{
    public interface IChecklistRepository
    {
        Task AddAsync(Checklist checklist);

        Task<Checklist?> GetByIdAsync(Guid id);

        Task<List<Checklist>> GetAllAsync();

        void Update(Checklist checklist);

        void Delete(Checklist checklist);

        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
