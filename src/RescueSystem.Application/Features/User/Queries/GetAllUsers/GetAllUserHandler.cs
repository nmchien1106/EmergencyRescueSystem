using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using RescueSystem.Application.DTOs.User;
using RescueSystem.Application.Interfaces.Respositories;

namespace RescueSystem.Application.Features.User.Queries.GetAllUser
{
    public class GetAllUserHandler : IRequestHandler<GetAllUserQuery, List<UserDTO>>
    {
        private readonly IUserRepository _userRepository;
        public GetAllUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<List<UserDTO>> Handle(GetAllUserQuery req, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllUsersAsync();
            var userDTOs = new List<UserDTO>();
            foreach (var user in users)
            {
                var roles = await _userRepository.GetUserRolesAsync(user);
                userDTOs.Add(new UserDTO
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    UserName = user.UserName,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address,
                    DateOfBirth = user.DateOfBirth,
                    Avatar = user.Avatar,
                    Roles = roles.ToList()
                });
            }
            return userDTOs;
        }
    }
}
