using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RescueSystem.Domain.Entities;
using RescueSystem.Infrastructure.Data;

namespace RescueSystem.Api.Seeders
{
    public static class ApplicationSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider, ILogger logger)
        {
            var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            try
            {
                await dbContext.Database.MigrateAsync();
                await SeedRoles(roleManager);
                await SeedAdminUser(userManager);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Đã có lỗi xảy ra trong quá trình migrate hoặc seed dữ liệu");
            }
        }

        private static async Task SeedRoles(RoleManager<ApplicationRole> roleManager)
        {
            var roles = new[] { "Citizen", "Rescuer", "Dispatcher", "Commander" };

            foreach (var roleName in roles)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new ApplicationRole
                    {
                        Name = roleName,
                        Description = $"{roleName} role"
                    });
                }
            }
        }

        private static async Task SeedAdminUser(UserManager<ApplicationUser> userManager)
        {
            const string adminEmail = "admin@rescuesystem.com";
            const string adminPassword = "Admin@123456";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                var newAdminUser = new ApplicationUser
                {
                    UserName = "admin",
                    Email = adminEmail,
                    FullName = "System Administrator",
                    IsActive = true,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(newAdminUser, adminPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newAdminUser, "Commander");
                }
            }
        }
    }
}
