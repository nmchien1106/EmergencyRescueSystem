using System.Data;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using RescueSystem.Domain.Enums;

namespace RescueSystem.Application.Features.Request.Commands.ChangeRequestStatus
{
    public class ChangeRequestStatusValidator : AbstractValidator<ChangeRequestStatusCommand>
    {
        public ChangeRequestStatusValidator()
        {
            RuleFor(x => x.RequestId)
                .NotEmpty().WithMessage("RequestId là bắt buộc");
            RuleFor(x => x.NewStatus)
                .IsInEnum().WithMessage("Trạng thái mới không hợp lệ");
        }
    }
}
