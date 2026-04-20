using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using RescueSystem.Application.Common.Exception;
using RescueSystem.Application.DTOs.Auth;
using RescueSystem.Application.Interfaces.Respositories;

namespace RescueSystem.Application.Features.Auth.Queries.Profile
{
    public class ProfileHandler : IRequestHandler<ProfileQuery, ProfileResponse>
    {
        private readonly IUserRepository _userRepository;
        public ProfileHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ProfileResponse> Handle(ProfileQuery request, CancellationToken cancellationToken)
        {
            var foundUser = await _userRepository.GetUserByIdAsync(request.UserId);
            if (foundUser == null)
            {
                throw new NotFoundException("User không tồn tại.");
            }

            var roles = await _userRepository.GetUserRolesAsync(foundUser);

            return new ProfileResponse
            {
                Id = foundUser.Id,
                Fullname = foundUser.FullName,
                Email = foundUser.Email,
                Roles = roles
            };
        }
    }
}
