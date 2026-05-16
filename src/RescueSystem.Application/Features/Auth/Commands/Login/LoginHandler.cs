using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Identity;
using RescueSystem.Application.Common.Exception;
using RescueSystem.Application.Common.Interfaces.Services;
using RescueSystem.Application.DTOs.Auth;
using RescueSystem.Application.Interfaces.Respositories;
using RescueSystem.Domain.Entities;
using RescueSystem.Application.DTOs.Auth;

namespace RescueSystem.Application.Features.Auth.Commands.Login
{
    public class LoginHandler : IRequestHandler<LoginCommand, AuthResponse>
    {
        private readonly IUserRepository userRepository;
        private readonly ITokenService tokenService;


        public LoginHandler(IUserRepository userRepository, ITokenService tokenService)
        {
            this.userRepository = userRepository;
            this.tokenService = tokenService;
        }

        public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetUserByEmailAsync(request.Email);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            var isPasswordValid = await userRepository.CheckPasswordAsync(user, request.Password);
            if (!isPasswordValid)
            {
                throw new BadRequestException("Invalid password or email! Please try again");
            }


            var roles = await userRepository.GetUserRolesAsync(user);
            var token = tokenService.GenerateToken(user.Id.ToString(), user.Email, roles);
            return new AuthResponse
            {
                AccessToken = token,
                RefreshToken = null,           // sau này thêm refresh token service
                ExpiresIn = 3600,
                User = new AuthUserDTO
                {
                    Id = user.Id,
                    Email = user.Email ?? string.Empty,
                    FullName = user.FullName,
                    PhoneNumber = user.PhoneNumber,
                    Roles = roles,
                },
            };
        }
    }
}
