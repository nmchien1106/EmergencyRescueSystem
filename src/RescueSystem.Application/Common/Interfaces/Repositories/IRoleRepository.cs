using RescueSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.Common.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        Task<List<ApplicationRole>> GetAllAsync();
        Task<ApplicationRole?> GetByIdAsync(Guid id);
        Task<ApplicationRole?> GetByNameAsync(string name);

        Task<bool> CreateAsync(ApplicationRole role);
        Task<bool> UpdateAsync(ApplicationRole role);
        Task<bool> DeleteAsync(ApplicationRole role);
    }
}
