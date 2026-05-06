using Microsoft.EntityFrameworkCore;
using RescueSystem.Application.Common.Interfaces.Services;
using RescueSystem.Domain.Entities;
using RescueSystem.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Infrastructure.Persistence.Services
{
    public class OtpService : IOtpService
    {
        private readonly ApplicationDbContext _context;

        public OtpService(ApplicationDbContext context)
        {
            _context = context;
        }

        public string GenerateOtp()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        public async Task SaveOtpAsync(string email, string otp)
        {
            var otpEntity = new OtpCode
            {
                Id = Guid.NewGuid(),
                Email = email,
                Code = otp,
                ExpireAt = DateTime.UtcNow.AddMinutes(5),
                IsUsed = false
            };

            _context.OtpCodes.Add(otpEntity);
            await _context.SaveChangesAsync();


        }

        public async Task<bool> ValidateOtpAsync(string email, string otp)
        {
            var otpEntity = await _context.OtpCodes
                .Where(x => x.Email == email && x.Code == otp && !x.IsUsed)
                .OrderByDescending(x => x.ExpireAt)
                .FirstOrDefaultAsync();

            if (otpEntity == null)
                return false;

            if (otpEntity.ExpireAt < DateTime.UtcNow)
                return false;

            otpEntity.IsUsed = true;
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
