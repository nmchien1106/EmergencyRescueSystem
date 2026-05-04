using MediatR;
using RescueSystem.Application.Common.Interfaces.Services;
using RescueSystem.Application.Interfaces.Respositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.Features.Auth.Commands.ResetPassword
{
    public class ResetPasswordHandler : IRequestHandler<ResetPasswordCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IOtpService _otpService;

        public ResetPasswordHandler(
            IUserRepository userRepository,
            IOtpService otpService)
        {
            _userRepository = userRepository;
            _otpService = otpService;
        }

        public async Task<string> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            // 1. check user
            var user = await _userRepository.GetUserByEmailAsync(request.Email);
            if (user == null)
                throw new Exception("User not found");

            // 2. validate OTP
            var isValid = await _otpService.ValidateOtpAsync(request.Email, request.Otp);
            if (!isValid)
                throw new Exception("OTP invalid or expired");

            // 3. update password
            await _userRepository.UpdatePasswordAsync(user, request.NewPassword);

            return "Reset password successfully";
        }
    }
}
