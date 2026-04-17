using MediatR;
using RescueSystem.Application.Interfaces.Respositories;
using RescueSystem.Application.Common.Exception;
using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.Features.User.Commands.UpdateUser
{
    public class UpdateUserHandler(IUserRepository userRepository)
        : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserProfileByIdAsync(request.Id);

            if (user == null)
                throw new NotFoundException("User not found");

            user = new Domain.Entities.ApplicationUser
            {
                Id = request.Id,
                FullName = request.FullName ?? user.FullName,
                PhoneNumber = request.PhoneNumber ?? user.PhoneNumber,
                Address = request.Address ?? user.Address,
                DateOfBirth = request.DateOfBirth ?? user.DateOfBirth,
                Avatar = request.Avatar ?? user.Avatar,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt
            };

            user.UpdatedAt = DateTime.UtcNow;

            await _userRepository.UpdateUserAsync(user);

            return true;
        }
    }
}
