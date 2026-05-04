using FluentValidation;

namespace RescueSystem.Application.Features.Auth.Commands.ResetPassword
{
    public class ResetPasswordValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Vui lòng nhập Email")
                .EmailAddress().WithMessage("Email không hợp lệ");

            RuleFor(x => x.Otp)
                .NotEmpty().WithMessage("OTP không được để trống")
                .Length(6).WithMessage("OTP phải có 6 chữ số");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("Vui lòng nhập mật khẩu mới")
                .MinimumLength(6).WithMessage("Mật khẩu phải có ít nhất 6 ký tự");
        }
    }
}