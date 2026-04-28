using FluentValidation;
using RescueSystem.Application.DTOs.Request;
using RescueSystem.Domain.Enums;

namespace RescueSystem.Application.Features.Request.Commands.CreateRequest
{
    public class CreateRequestValidator : AbstractValidator<CreateRequestCommand>
    {
        public CreateRequestValidator()
        {
            RuleFor(x => x.UserId)
                .Must(id => id.HasValue && id.Value != Guid.Empty)
                .WithMessage("UserId không hợp lệ");

            RuleFor(x => x.EmergencyType)
                .IsInEnum().WithMessage("Loại tình huống khẩn cấp không hợp lệ");

            RuleFor(x => x.Status)
                .Equal(RequestStatus.PENDING)
                .WithMessage("Trạng thái ban đầu của yêu cầu phải là PENDING");

            RuleFor(x => x.LocationId)
                .NotEmpty().WithMessage("LocationId là bắt buộc");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Mô tả không được bỏ trống")
                .MaximumLength(2000).WithMessage("Mô tả không được vượt quá 2000 ký tự");

            RuleFor(x => x.Media)
                .Must(media => media == null || media.Count <= 10)
                .WithMessage("Số lượng media tối đa là 10");

            RuleForEach(x => x.Media!)
                .SetValidator(new RequestMediaValidator())
                .When(x => x.Media != null && x.Media.Count > 0);
        }
    }

    public class RequestMediaValidator : AbstractValidator<RequestMediaDTO>
    {
        public RequestMediaValidator()
        {
            RuleFor(x => x.SescueUrl)
                .NotEmpty().WithMessage("URL media không được bỏ trống")
                .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
                .WithMessage("URL media không hợp lệ");

            RuleFor(x => x.PublicId)
                .NotEmpty().WithMessage("PublicId không được bỏ trống")
                .MaximumLength(255).WithMessage("PublicId không được vượt quá 255 ký tự");

            RuleFor(x => x.ResourceType)
                .IsInEnum().WithMessage("Loại media không hợp lệ");
        }
    }
}
