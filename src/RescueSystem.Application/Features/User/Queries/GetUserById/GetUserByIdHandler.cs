using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using RescueSystem.Application.Common.Exception;
using RescueSystem.Application.DTOs.User;
using RescueSystem.Application.Interfaces.Respositories;
using RescueSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.Features.User.Queries.GetUserById
{
    public class GetUserByIdHandler(IUserRepository userRepository) : IRequestHandler<GetUserByIdQuery, UserDTO>
    {
      

        private readonly ILogger<GetUserByIdHandler> _logger;
        private readonly IMapper _mapper;

        public async Task<UserDTO> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetUserProfileByIdAsync(request.Id);

            if (user == null)
            {
                _logger.LogWarning("User with id {UserId} not found", request.Id);
                throw new NotFoundException("Do not exit");

            }

            var roles = await userRepository.GetUserRolesAsync(user);

            var res = new UserDTO
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
            };
            return res;
        }
    }
}


