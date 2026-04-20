using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.Common.Interfaces.Services
{
    public interface ITokenService
    {
        string GenerateToken(string userId, string email, List<string> roles);
    }
}
