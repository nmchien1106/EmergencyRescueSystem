using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace RescueSystem.Application.Features.Request.Queries.GetRequestById
{
    public class GetRequestByIdValidator : AbstractValidator<GetRequestByIdQuery>
    {
        public GetRequestByIdValidator()
        {
            RuleFor(x => x.RequestId).NotEmpty().WithMessage("Mã gửi yêu cầu cứu hộ không được để trống");
        }
    }
}
