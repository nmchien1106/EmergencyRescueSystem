using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RescueSystem.Application.Interfaces.Respositories;
using RescueSystem.Domain.Entities;
using RescueSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        // Create User with password
        public async Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }
        // Create User with password and roles
        public async Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password, IList<string> roles)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded && roles != null)
            {
                await _userManager.AddToRolesAsync(user, roles);
            }
            return result;
        }
        // Create User without password
        public async Task<IdentityResult> CreateUserAsync(ApplicationUser user)
        {
            return await _userManager.CreateAsync(user);
        }

        // check password
        public async Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        // Find user by email
        public async Task<ApplicationUser?> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        // Find user by id
        public async Task<ApplicationUser?> GetUserByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        // Confirm email
        public async Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string token)
        {
            var result = await _userManager.ConfirmEmailAsync(user, token);
            return result;
        }

        // Update user
        public async Task UpdateUserAsync(ApplicationUser user)
        {
            // 1. Lấy "thằng gốc" đang có đầy đủ SecurityStamp từ DB lên
            var existingUser = await _userManager.FindByIdAsync(user.Id.ToString());

            if (existingUser == null)
            {
                throw new Exception("User not found");
            }

            // 2. Cập nhật các thông tin thay đổi vào "thằng gốc"
            existingUser.FullName = user.FullName;
            existingUser.PhoneNumber = user.PhoneNumber;
            existingUser.Address = user.Address;
            existingUser.DateOfBirth = user.DateOfBirth;
            existingUser.Avatar = user.Avatar;

            // 3. Lưu "thằng gốc" đã được cập nhật
            var result = await _userManager.UpdateAsync(existingUser);
            if (!result.Succeeded)
            {
                throw new Exception("Failed to update user");
            }
        }

        //public async Task<IdentityResult> UpdateUserAsync(ApplicationUser user)
        //{
        //    return await _userManager.UpdateAsync(user);
        //}


        // Delete user
        public async Task<IdentityResult> DeleteUserAsync(ApplicationUser user)
        {
            return await _userManager.DeleteAsync(user);
        }
        // get all users
        public async Task<List<ApplicationUser>> GetAllUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        // get user by profile id
        public async Task<ApplicationUser?> GetUserProfileByIdAsync(Guid userId)
        {
            return await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }

        // check isEmailConfirmed
        public async Task<bool> IsEmailConfirmedAsync(ApplicationUser user)
        {
            return await _userManager.IsEmailConfirmedAsync(user);
        }
        // get user roles
        public async Task<IList<string>> GetUserRolesAsync(ApplicationUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }
    }
}
