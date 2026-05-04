using Microsoft.Extensions.Configuration;
using RescueSystem.Application.Common.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace RescueSystem.Infrastructure.Persistence.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendOtpEmail(string toEmail, string otp)
        {
            var smtp = new SmtpClient(
                _config["EmailSettings:SmtpServer"],
                int.Parse(_config["EmailSettings:Port"])
            )
            {
                Credentials = new NetworkCredential(
                    _config["EmailSettings:SenderEmail"],
                    _config["EmailSettings:Password"]
                ),
                EnableSsl = true
            };

            var mail = new MailMessage
            {
                From = new MailAddress(_config["EmailSettings:SenderEmail"]),
                Subject = "OTP Reset Password",
                Body = $"Your OTP is: {otp}",
            };

            mail.To.Add(toEmail);

            await smtp.SendMailAsync(mail);
        }
    }
}
