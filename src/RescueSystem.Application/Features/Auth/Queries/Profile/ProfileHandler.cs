using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using RescueSystem.Application.Common.Exception;
using RescueSystem.Application.DTOs.Address;
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
            var address = await _userRepository.GetAddressByUserIdAsync(foundUser.Id);

            return new ProfileResponse
            {
                Id = foundUser.Id,
                Fullname = foundUser.FullName,
                Email = foundUser.Email,
                PhoneNumber = foundUser.PhoneNumber ?? string.Empty,
                Address = address == null ? null : new AddressDTO
                {
                    Street = address.Street,
                    City = address.City,
                    District = address.District,
                    GPS = address.GPS
                },
                Roles = roles
            };
        }
    }
}
