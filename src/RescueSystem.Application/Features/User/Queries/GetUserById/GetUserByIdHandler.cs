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
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserDTO>
    {
        public GetUserByIdHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetUserByIdHandler> _logger;

        public async Task<UserDTO> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserProfileByIdAsync(request.Id);

            if (user == null)
            {
                _logger.LogWarning("User with id {UserId} not found", request.Id);
                throw new NotFoundException("Do not exit");

            }

            var response = _mapper.Map<UserDTO>(user);
            return response;
        }
    }
}


