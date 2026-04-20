using RescueSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace RescueSystem.Application.Interfaces.Respositories
{
    public interface IUserRepository
    {
        // Create User Methods
        Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password);
        Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password, IList<string> roles);
        Task<IdentityResult> CreateUserAsync(ApplicationUser user);

        // Read/Get User Methods
        Task<ApplicationUser?> GetUserByEmailAsync(string email);
        Task<ApplicationUser?> GetUserByIdAsync(string id);
        Task<ApplicationUser?> GetUserProfileByIdAsync(Guid userId);
        Task<List<ApplicationUser>> GetAllUsersAsync();

        // Password & Email Methods
        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
        Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string token);
        Task<bool> IsEmailConfirmedAsync(ApplicationUser user);

        // Update User Method
        Task UpdateUserAsync(ApplicationUser user);

        // Delete User Method
        Task<IdentityResult> DeleteUserAsync(ApplicationUser user);

        // Role Methods
        Task<List<string>> GetUserRolesAsync(ApplicationUser user);
    }
}
