using RescueSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace RescueSystem.Application.Interfaces.Respositories
{
    public interface IUserRepository
    {
        Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password, IList<string> roles= null);
        Task<List<ApplicationUser>> GetAllUsersAsync();
        Task<IList<string>> GetUserRolesAsync(ApplicationUser user);
        Task<ApplicationUser?> GetUserProfileByIdAsync(Guid userId);
        //Task<IdentityResult> UpdateUserAsync(ApplicationUser user);
        Task UpdateUserAsync(ApplicationUser user);
        Task<IdentityResult> DeleteUserAsync(ApplicationUser user);
    }
}
