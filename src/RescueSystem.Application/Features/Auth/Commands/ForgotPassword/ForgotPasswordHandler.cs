using MediatR;
using RescueSystem.Application.Common.Exception;
using RescueSystem.Application.Common.Interfaces.Services;
using RescueSystem.Application.DTOs.Auth;
using RescueSystem.Application.Interfaces.Respositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.Features.Auth.Commands.ForgotPassword
{
    public class ForgotPasswordHandler : IRequestHandler<ForgotPasswordCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IOtpService _otpService;
        private readonly IEmailService _emailService;

        public ForgotPasswordHandler(
            IUserRepository userRepository,
            IOtpService otpService,
            IEmailService emailService)
        {
            _userRepository = userRepository;
            _otpService = otpService;
            _emailService = emailService;
        }

        public async Task<string> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            // 1. check email tồn tại
            var user = await _userRepository.GetUserByEmailAsync(request.Email);
            if (user == null)
                throw new Exception("Email not found");

            // 2. generate OTP
            var otp = _otpService.GenerateOtp();

            // 3. lưu OTP (5 phút)
            await _otpService.SaveOtpAsync(request.Email, otp);

            Console.WriteLine("Sending OTP: " + otp);
            // 4. gửi mail
            await _emailService.SendOtpEmail(request.Email, otp);

            return "OTP sent to your email";
        }
    }
}

