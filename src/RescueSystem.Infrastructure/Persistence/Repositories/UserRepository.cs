using System;
using System.Collections.Generic;
using System.Text;
using RescueSystem.Application.Interfaces.Respositories;
using RescueSystem.Domain.Entities;
using RescueSystem.Infrastructure.Data;

namespace RescueSystem.Infrastructure.Persistence.Responsitories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _ctx;
        public UserRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task CreateUser(ApplicationUser user)
        {
            _ctx.Add(user);
            await _ctx.SaveChangesAsync();
        }
    }
}
