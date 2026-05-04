using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.Common.Interfaces.Services
{
    public interface IOtpService
    {
        string GenerateOtp();
        Task SaveOtpAsync(string email, string otp);
        Task<bool> ValidateOtpAsync(string email, string otp);
    }
}
