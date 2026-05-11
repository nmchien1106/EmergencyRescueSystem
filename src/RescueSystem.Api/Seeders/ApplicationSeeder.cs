using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RescueSystem.Domain.Entities;
using RescueSystem.Domain.Enums;
using RescueSystem.Infrastructure.Persistence;

namespace RescueSystem.Api.Seeders
{
    public static class ApplicationSeeder
    {
        private const int BatchSize = 10;

        public static async Task SeedAsync(IServiceProvider serviceProvider, ILogger logger)
        {
            var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            try
            {
                await dbContext.Database.MigrateAsync();
                await SeedRoles(roleManager);
                await SeedUsers(userManager);
                await SeedLocations(dbContext);
                await SeedRescueTeams(dbContext, userManager);
                await SeedRequests(dbContext, userManager);
                await SeedMissions(dbContext, userManager);
                await SeedMissionHistories(dbContext, userManager);
                await SeedReports(dbContext);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Đã có lỗi xảy ra trong quá trình migrate hoặc seed dữ liệu");
            }
        }

        private static async Task SeedRoles(RoleManager<ApplicationRole> roleManager)
        {
            var roles = new[]
            {
                "Citizen",
                "Rescuer",
                "RescuerLeader",
                "Dispatcher",
                "Commander"
            };

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

        private static async Task SeedUsers(UserManager<ApplicationUser> userManager)
        {
            const string adminEmail = "admin@rescuesystem.com";
            const string adminPassword = "Admin@123456";

            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var admin = new ApplicationUser
                {
                    UserName = "admin",
                    Email = adminEmail,
                    FullName = "System Administrator",
                    PhoneNumber = "0100000000",
                    Address = "Hà Nội, Việt Nam",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    IsActive = true,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(admin, adminPassword);
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(admin, "Commander");
            }

            for (var i = 1; i <= BatchSize; i++)
            {
                var suffix = i.ToString("D2");
                await EnsureUserInRoleAsync(userManager,
                    email: $"citizen{suffix}@rescuesystem.com",
                    userName: $"citizen{suffix}",
                    fullName: $"Citizen Seed {suffix}",
                    phone: $"090100{suffix}",
                    password: "Citizen@123",
                    role: "Citizen",
                    birthDayOffset: i);

                await EnsureUserInRoleAsync(userManager,
                    email: $"rescuer{suffix}@rescuesystem.com",
                    userName: $"rescuer{suffix}",
                    fullName: $"Rescuer Seed {suffix}",
                    phone: $"090200{suffix}",
                    password: "Rescuer@123",
                    role: "Rescuer",
                    birthDayOffset: i + 20);

                await EnsureUserInRoleAsync(userManager,
                    email: $"rleader{suffix}@rescuesystem.com",
                    userName: $"rleader{suffix}",
                    fullName: $"Rescuer Leader Seed {suffix}",
                    phone: $"090300{suffix}",
                    password: "RescuerLeader@123",
                    role: "RescuerLeader",
                    birthDayOffset: i + 40);

                await EnsureUserInRoleAsync(userManager,
                    email: $"dispatcher{suffix}@rescuesystem.com",
                    userName: $"dispatcher{suffix}",
                    fullName: $"Dispatcher Seed {suffix}",
                    phone: $"090400{suffix}",
                    password: "Dispatcher@123",
                    role: "Dispatcher",
                    birthDayOffset: i + 60);

                await EnsureUserInRoleAsync(userManager,
                    email: $"commander{suffix}@rescuesystem.com",
                    userName: $"commander{suffix}",
                    fullName: $"Commander Seed {suffix}",
                    phone: $"090500{suffix}",
                    password: "Commander@123",
                    role: "Commander",
                    birthDayOffset: i + 80);
            }
        }

        private static async Task EnsureUserInRoleAsync(
            UserManager<ApplicationUser> userManager,
            string email,
            string userName,
            string fullName,
            string phone,
            string password,
            string role,
            int birthDayOffset)
        {
            if (await userManager.FindByEmailAsync(email) != null)
                return;

            var user = new ApplicationUser
            {
                UserName = userName,
                Email = email,
                FullName = fullName,
                PhoneNumber = phone,
                Address = "Hà Nội, Việt Nam",
                DateOfBirth = new DateTime(1995, 6, 15).AddDays(birthDayOffset),
                IsActive = true,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, password);
            if (result.Succeeded)
                await userManager.AddToRoleAsync(user, role);
        }

        private static async Task SeedLocations(ApplicationDbContext context)
        {
            if (await context.Locations.AnyAsync())
                return;

            var baseLat = 21.0285;
            var baseLng = 105.8542;
            var locations = new List<Location>();

            for (var i = 0; i < BatchSize; i++)
            {
                var offset = i * 0.002;
                locations.Add(new Location
                {
                    Latitude = baseLat + offset,
                    Longitude = baseLng + offset,
                    Address = $"Seed địa điểm {i + 1}, Hoàn Kiếm, Hà Nội",
                    Landmark = $"Mốc seed #{i + 1}"
                });
            }

            await context.Locations.AddRangeAsync(locations);
            await context.SaveChangesAsync();
        }

        private static async Task SeedRescueTeams(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            if (await context.RescueTeams.AnyAsync())
                return;

            var leaders = (await userManager.GetUsersInRoleAsync("RescuerLeader"))
                .OrderBy(u => u.Email)
                .ToList();
            var rescuers = (await userManager.GetUsersInRoleAsync("Rescuer"))
                .OrderBy(u => u.Email)
                .ToList();
            var locations = await context.Locations.OrderBy(l => l.CreatedAt).ToListAsync();

            if (leaders.Count < BatchSize || rescuers.Count < BatchSize || locations.Count < BatchSize)
                return;

            var teams = new List<RescueTeam>();
            for (var i = 0; i < BatchSize; i++)
            {
                teams.Add(new RescueTeam
                {
                    TeamName = $"Seed Rescue Team {i + 1:D2}",
                    TeamLeaderId = leaders[i].Id,
                    BaseLocationId = locations[i].Id,
                    Status = i % 3 == 0 ? TeamStatus.ON_MISSION : TeamStatus.AVAILABLE
                });
            }

            await context.RescueTeams.AddRangeAsync(teams);
            await context.SaveChangesAsync();

            var createdTeams = await context.RescueTeams
                .Include(t => t.Members)
                .OrderBy(t => t.TeamName)
                .ToListAsync();

            for (var i = 0; i < BatchSize; i++)
            {
                var member = await context.Users.FindAsync(rescuers[i].Id);
                if (member != null && createdTeams.Count > i)
                    createdTeams[i].Members.Add(member);
            }

            await context.SaveChangesAsync();
        }

        private static async Task SeedRequests(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            if (await context.Requests.AnyAsync())
                return;

            var locations = await context.Locations.OrderBy(l => l.CreatedAt).ToListAsync();
            var citizens = (await userManager.GetUsersInRoleAsync("Citizen"))
                .OrderBy(u => u.Email)
                .ToList();

            if (citizens.Count < BatchSize || locations.Count < BatchSize)
                return;

            var statuses = new[]
            {
                RequestStatus.PENDING,
                RequestStatus.ACCEPTED,
                RequestStatus.IN_PROGRESS,
                RequestStatus.COMPLETED,
                RequestStatus.PENDING,
                RequestStatus.ACCEPTED,
                RequestStatus.IN_PROGRESS,
                RequestStatus.COMPLETED,
                RequestStatus.PENDING,
                RequestStatus.ACCEPTED
            };

            var requests = new List<RescueRequest>();
            for (var i = 0; i < BatchSize; i++)
            {
                requests.Add(new RescueRequest
                {
                    UserId = citizens[i].Id,
                    EmergencyType = (EmergencyType)((i % 8) + 1),
                    Priority = (Priority)((i % 4) + 1),
                    Status = statuses[i],
                    LocationId = locations[i].Id,
                    Description = $"Yêu cầu cứu hộ seed #{i + 1} — liên kết citizen & location."
                });
            }

            await context.Requests.AddRangeAsync(requests);
            await context.SaveChangesAsync();
        }

        private static async Task SeedMissions(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            if (await context.Missions.AnyAsync())
                return;

            var requests = await context.Requests.OrderBy(r => r.CreatedAt).ToListAsync();
            var teams = await context.RescueTeams.OrderBy(t => t.TeamName).ToListAsync();
            var dispatchers = (await userManager.GetUsersInRoleAsync("Dispatcher"))
                .OrderBy(u => u.Email)
                .ToList();

            if (requests.Count < BatchSize || teams.Count < BatchSize || dispatchers.Count < BatchSize)
                return;

            // Request[0] giữ PENDING không mission; các request 1..6 có mission để nối dispatcher + team
            const int missionCount = 6;
            var missions = new List<Mission>();
            var missionStatuses = new[]
            {
                MissionStatus.ASSIGNED,
                MissionStatus.EN_ROUTE,
                MissionStatus.ON_SITE,
                MissionStatus.IN_PROGRESS,
                MissionStatus.COMPLETED,
                MissionStatus.COMPLETED
            };

            for (var i = 0; i < missionCount; i++)
            {
                var req = requests[i + 1];
                var start = DateTime.UtcNow.AddHours(-(i + 1) * 3);
                var end = missionStatuses[i] == MissionStatus.COMPLETED
                    ? start.AddHours(2)
                    : (DateTime?)null;

                missions.Add(new Mission
                {
                    RequestId = req.Id,
                    DispatcherId = dispatchers[i].Id,
                    RescueTeamId = teams[i].Id,
                    StartTime = start,
                    EndTime = end,
                    Status = missionStatuses[i]
                });
            }

            await context.Missions.AddRangeAsync(missions);
            await context.SaveChangesAsync();
        }

        private static async Task SeedMissionHistories(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            if (await context.MissionHistories.AnyAsync())
                return;

            var missions = await context.Missions
                .Include(m => m.RescueTeam)
                .OrderBy(m => m.StartTime)
                .ToListAsync();

            var commanders = (await userManager.GetUsersInRoleAsync("Commander"))
                .OrderBy(u => u.Email)
                .ToList();

            if (missions.Count == 0 || commanders.Count == 0)
                return;

            var histories = new List<MissionHistory>();
            var commanderIdx = 0;

            foreach (var mission in missions)
            {
                var changedBy = commanders[commanderIdx % commanders.Count];
                commanderIdx++;

                histories.Add(new MissionHistory
                {
                    MissionId = mission.Id,
                    FromStatus = null,
                    ToStatus = MissionStatus.ASSIGNED,
                    ChangedById = changedBy.Id,
                    Note = "Seed: tạo nhiệm vụ"
                });

                if (mission.Status != MissionStatus.ASSIGNED)
                {
                    histories.Add(new MissionHistory
                    {
                        MissionId = mission.Id,
                        FromStatus = MissionStatus.ASSIGNED,
                        ToStatus = mission.Status,
                        ChangedById = changedBy.Id,
                        Note = "Seed: cập nhật trạng thái hiện tại"
                    });
                }
            }

            await context.MissionHistories.AddRangeAsync(histories);
            await context.SaveChangesAsync();
        }

        private static async Task SeedReports(ApplicationDbContext context)
        {
            if (await context.Reports.AnyAsync())
                return;

            var completedMissions = await context.Missions
                .Where(m => m.Status == MissionStatus.COMPLETED)
                .Include(m => m.RescueTeam!)
                    .ThenInclude(t => t.Members)
                .OrderBy(m => m.StartTime)
                .ToListAsync();

            if (completedMissions.Count == 0)
                return;

            var reports = new List<Report>();

            foreach (var mission in completedMissions)
            {
                var authorId = mission.RescueTeam?.Members.FirstOrDefault()?.Id;
                if (authorId == null)
                    continue;

                reports.Add(new Report
                {
                    MissionId = mission.Id,
                    CreatedById = authorId.Value,
                    Content = $"Báo cáo hoàn thành seed — mission {mission.Id}",
                    AttachmentUrl = string.Empty,
                    Type = ReportType.COMPLETION
                });
            }

            if (reports.Count > 0)
            {
                await context.Reports.AddRangeAsync(reports);
                await context.SaveChangesAsync();
            }
        }
    }
}
