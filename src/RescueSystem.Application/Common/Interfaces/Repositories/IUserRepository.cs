using RescueSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace RescueSystem.Application.Interfaces.Respositories
{
    public interface IUserRepository
    {
        public Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password);
        Task<List<ApplicationUser>> GetAllUsersAsync();
        Task<IList<string>> GetUserRolesAsync(ApplicationUser user);
        Task<ApplicationUser?> GetUserProfileByIdAsync(Guid userId);
    }
}
