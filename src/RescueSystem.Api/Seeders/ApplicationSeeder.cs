using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RescueSystem.Domain.Entities;
using RescueSystem.Domain.Enums;
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
                await SeedUsers(userManager, dbContext);
                await SeedLocations(dbContext);
                await SeedRescueTeams(dbContext, userManager);
                await SeedRequests(dbContext, userManager);
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

        private static async Task SeedUsers(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            // Seed Admin User
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
                    PhoneNumber = "0123456789",
                    Address = "Hà Nội, Việt Nam",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    IsActive = true,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(newAdminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newAdminUser, "Commander");
                }
            }

            // Seed Rescuer Users
            var rescuerEmails = new[] { "rescuer1@rescuesystem.com", "rescuer2@rescuesystem.com", "rescuer3@rescuesystem.com" };
            foreach (var email in rescuerEmails)
            {
                var rescuerUser = await userManager.FindByEmailAsync(email);
                if (rescuerUser == null)
                {
                    var newRescuerUser = new ApplicationUser
                    {
                        UserName = email.Split('@')[0],
                        Email = email,
                        FullName = $"Rescuer {email.Split('@')[0]}",
                        PhoneNumber = $"010000000{Array.IndexOf(rescuerEmails, email) + 1}",
                        Address = "Hà Nội, Việt Nam",
                        DateOfBirth = new DateTime(1995, 5, 15),
                        IsActive = true,
                        EmailConfirmed = true
                    };

                    var result = await userManager.CreateAsync(newRescuerUser, "Rescuer@123");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(newRescuerUser, "Rescuer");
                    }
                }
            }

            // Seed Dispatcher Users
            var dispatcherEmails = new[] { "dispatcher1@rescuesystem.com", "dispatcher2@rescuesystem.com" };
            foreach (var email in dispatcherEmails)
            {
                var dispatcherUser = await userManager.FindByEmailAsync(email);
                if (dispatcherUser == null)
                {
                    var newDispatcherUser = new ApplicationUser
                    {
                        UserName = email.Split('@')[0],
                        Email = email,
                        FullName = $"Dispatcher {email.Split('@')[0]}",
                        PhoneNumber = $"020000000{Array.IndexOf(dispatcherEmails, email) + 1}",
                        Address = "Hà Nội, Việt Nam",
                        DateOfBirth = new DateTime(1992, 3, 20),
                        IsActive = true,
                        EmailConfirmed = true
                    };

                    var result = await userManager.CreateAsync(newDispatcherUser, "Dispatcher@123");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(newDispatcherUser, "Dispatcher");
                    }
                }
            }

            // Seed Citizen Users
            var citizenEmails = new[] { "citizen1@rescuesystem.com", "citizen2@rescuesystem.com", "citizen3@rescuesystem.com" };
            foreach (var email in citizenEmails)
            {
                var citizenUser = await userManager.FindByEmailAsync(email);
                if (citizenUser == null)
                {
                    var newCitizenUser = new ApplicationUser
                    {
                        UserName = email.Split('@')[0],
                        Email = email,
                        FullName = $"Citizen {email.Split('@')[0]}",
                        PhoneNumber = $"030000000{Array.IndexOf(citizenEmails, email) + 1}",
                        Address = "Hà Nội, Việt Nam",
                        DateOfBirth = new DateTime(2000, 7, 10),
                        IsActive = true,
                        EmailConfirmed = true
                    };

                    var result = await userManager.CreateAsync(newCitizenUser, "Citizen@123");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(newCitizenUser, "Citizen");
                    }
                }
            }

            await context.SaveChangesAsync();
        }

        private static async Task SeedLocations(ApplicationDbContext context)
        {
            if (await context.Locations.AnyAsync())
                return;

            var locations = new List<Location>
            {
                new Location
                {
                    Latitude = 21.0285,
                    Longitude = 105.8542,
                    Address = "72 Trần Hưng Đạo, Hoan Kiem, Ha Noi",
                    Landmark = "Hoan Kiem Lake"
                },
                new Location
                {
                    Latitude = 21.0073,
                    Longitude = 105.8467,
                    Address = "1 Phan Boi Chau, Hoan Kiem, Ha Noi",
                    Landmark = "Thang Long Citadel"
                },
                new Location
                {
                    Latitude = 21.0355,
                    Longitude = 105.8597,
                    Address = "40 Hang Bac, Hoan Kiem, Ha Noi",
                    Landmark = "Old Quarter"
                },
                new Location
                {
                    Latitude = 20.9730,
                    Longitude = 105.8244,
                    Address = "Vo Nhan Tru, Hai Ba Trung, Ha Noi",
                    Landmark = "Tran Quoc Pagoda"
                },
                new Location
                {
                    Latitude = 21.0489,
                    Longitude = 105.8408,
                    Address = "Ba Dinh, Ha Noi",
                    Landmark = "Ba Dinh Square"
                }
            };

            await context.Locations.AddRangeAsync(locations);
            await context.SaveChangesAsync();
        }

        private static async Task SeedRescueTeams(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            if (await context.RescueTeams.AnyAsync())
                return;

            var locations = await context.Locations.ToListAsync();
            var rescuers = await userManager.GetUsersInRoleAsync("Rescuer");

            var teams = new List<RescueTeam>();

            if (rescuers.Count > 0 && locations.Count > 0)
            {
                teams.Add(new RescueTeam
                {
                    TeamName = "Fire Rescue Team Alpha",
                    TeamLeaderId = rescuers[0].Id,
                    BaseLocationId = locations[0].Id,
                    Status = TeamStatus.AVAILABLE
                });

                if (rescuers.Count > 1 && locations.Count > 1)
                {
                    teams.Add(new RescueTeam
                    {
                        TeamName = "Medical Response Team Beta",
                        TeamLeaderId = rescuers[1].Id,
                        BaseLocationId = locations[1].Id,
                        Status = TeamStatus.AVAILABLE
                    });
                }

                if (rescuers.Count > 2 && locations.Count > 2)
                {
                    teams.Add(new RescueTeam
                    {
                        TeamName = "Disaster Relief Team Gamma",
                        TeamLeaderId = rescuers[2].Id,
                        BaseLocationId = locations[2].Id,
                        Status = TeamStatus.AVAILABLE
                    });
                }

                await context.RescueTeams.AddRangeAsync(teams);
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedRequests(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            if (await context.Requests.AnyAsync())
                return;

            var locations = await context.Locations.ToListAsync();
            var citizens = await userManager.GetUsersInRoleAsync("Citizen");

            if (citizens.Count == 0 || locations.Count == 0)
                return;

            var requests = new List<RescueRequest>
            {
                new RescueRequest
                {
                    UserId = citizens[0].Id,
                    EmergencyType = EmergencyType.FIRE,
                    Priority = Priority.CRITICAL,
                    Status = RequestStatus.PENDING,
                    LocationId = locations[0].Id,
                    Description = "Cháy lớn tại khu phố cổ, cần sơ tán khẩn cấp"
                },
                new RescueRequest
                {
                    UserId = citizens[1].Id,
                    EmergencyType = EmergencyType.MEDICAL_EMERGENCY,
                    Priority = Priority.HIGH,
                    Status = RequestStatus.ACCEPTED,
                    LocationId = locations[1].Id,
                    Description = "Bệnh nhân gặp chấn thương nặng, cần cấp cứu ngay"
                },
                new RescueRequest
                {
                    UserId = citizens[2].Id,
                    EmergencyType = EmergencyType.FLOOD,
                    Priority = Priority.HIGH,
                    Status = RequestStatus.IN_PROGRESS,
                    LocationId = locations[2].Id,
                    Description = "Nước lũ dâng cao, cần sơ tán dân cư"
                },
                new RescueRequest
                {
                    UserId = citizens[0].Id,
                    EmergencyType = EmergencyType.TRAFFIC_EMERGENCY,
                    Priority = Priority.MEDIUM,
                    Status = RequestStatus.PENDING,
                    LocationId = locations[3].Id,
                    Description = "Tai nạn giao thông trên đường Tây Sơn"
                },
                new RescueRequest
                {
                    UserId = citizens[1].Id,
                    EmergencyType = EmergencyType.BUILDING_COLLAPSE,
                    Priority = Priority.CRITICAL,
                    Status = RequestStatus.COMPLETED,
                    LocationId = locations[4].Id,
                    Description = "Sập một phần tòa nhà cũ, có người bị kẹt"
                }
            };

            await context.Requests.AddRangeAsync(requests);
            await context.SaveChangesAsync();
        }
    }
}
