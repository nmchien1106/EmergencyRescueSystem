using System.Security.Cryptography.X509Certificates;
using FluentValidation;

namespace RescueSystem.Application.Features.RescueTeam.Commands.CreateRescueTeam
{
    public class CreateRescueTeamValidator:AbstractValidator<CreateRescueTeamCommand> 
    {
        public CreateRescueTeamValidator() {
            RuleFor(x => x.TeamName)
                .NotEmpty().WithMessage("Tên đội không được để trống")
                .MaximumLength(256).WithMessage("Tên đội không vượt quá 256 ký tự");

            RuleFor(x => x.TeamLeaderId)
                .NotEmpty().WithMessage("Vui lòng chỉ định Sĩ quan chỉ huy");
            
            RuleFor(x => x.BaseLocationId)
                .NotEmpty().WithMessage("Vui lòng chọn Vị trí căn cứ");
        }
    }
}