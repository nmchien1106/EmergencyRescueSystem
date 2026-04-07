using RescueSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.Interfaces.Respositories
{
    public interface IUserRepository
    {
        Task CreateUser(ApplicationUser user);
    }
}
