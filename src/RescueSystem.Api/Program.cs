using Autofac.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RescueSystem.Application;
using RescueSystem.Domain.Entities;
using RescueSystem.Infrastructure;
using RescueSystem.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Add Swagger services
builder.Services.AddSwaggerGen(c => 
c.EnableAnnotations());

builder.Services
    .AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

// Apply migrations and create database
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

    try
    {
        // Apply pending migrations
        dbContext.Database.Migrate();

        // Seed roles
        await SeedRoles(roleManager);

        // Seed default admin user
        await SeedAdminUser(userManager, roleManager);
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating or seeding the database.");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    // Enable Swagger UI
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Rescue System API v1");
        c.RoutePrefix = string.Empty; // Serve Swagger UI at root
    });

}

// Register global exception middleware early in the pipeline
app.UseMiddleware<RescueSystem.Api.Middlewares.GlobalExceptionMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

// Helper methods for seeding
static async Task SeedRoles(RoleManager<ApplicationRole> roleManager)
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

static async Task SeedAdminUser(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
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
