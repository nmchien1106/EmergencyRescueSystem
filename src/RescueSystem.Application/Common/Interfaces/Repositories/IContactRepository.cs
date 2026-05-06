using RescueSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.Common.Interfaces.Repositories
{
    public interface IContactRepository
    {
        Task<Contact?> GetByIdAsync(Guid id);
        Task<List<Contact>> GetByUserIdAsync(Guid userId);
        Task CreateAsync(Contact contact);
        Task UpdateAsync(Contact contact);
        Task DeleteAsync(Contact contact);
    }
}
