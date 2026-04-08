using MediatR;
using RescueSystem.Application.Common.Response;
using RescueSystem.Application.Interfaces.Respositories;
using RescueSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.Features.User.Commands
{
    public class CreateUserHandler(IUserRepository userRepository) : IRequestHandler<CreateUserCommand, Unit>
    {
        
        public async Task<Unit> Handle(CreateUserCommand req, CancellationToken cancellationToken)
        {
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                UserName = req.UserName,
                Email = req.Email,
                FullName = req.FullName,
                PhoneNumber = req.PhoneNumber,
                Address = req.Address,
                DateOfBirth = req.DateOfBirth,
                Avatar = req.Avatar,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            await userRepository.CreateUserAsync(user, req.Password);
            return Unit.Value;
        }
    }
}
