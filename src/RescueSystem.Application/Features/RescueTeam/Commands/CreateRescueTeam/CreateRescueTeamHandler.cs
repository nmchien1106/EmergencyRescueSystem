using MediatR;
using Microsoft.AspNetCore.Http;
using RescueSystem.Application.Common.Interfaces.Repositories;
using RescueSystem.Application.DTOs.RescueTeam;
using RescueSystem.Application.Features.RescueTeam.Commands.CreateRescueTeam;
using RescueSystem.Domain.Enums;
using RescueSystem.Domain.Entities;
using RescueSystem.Application.Interfaces.Respositories;
using System.Linq;

namespace RescueSystem.Application.Features.RescueTeam.Commands.CreateRescueTeam 
{
    public class CreateRescueTeamHandler: IRequestHandler<CreateRescueTeamCommand, RescueTeamDTO>
    {
        private readonly IRescueTeamRepository _rescueTeamRepository;
        private readonly IUserRepository _userRepository;


        public CreateRescueTeamHandler(IRescueTeamRepository rescueTeamRepository, IUserRepository userRepository)
        {
            _rescueTeamRepository = rescueTeamRepository;
            _userRepository = userRepository;
        }

        public async Task<RescueTeamDTO> Handle(CreateRescueTeamCommand request, CancellationToken cancellationToken)
        {
            var teamId = Guid.NewGuid();
            var rescueTeam = new RescueSystem.Domain.Entities.RescueTeam
            {
                Id = teamId,
                TeamName = request.TeamName,
                Description = request.Description, 
                TeamLeaderId = request.TeamLeaderId,
                BaseLocationId = request.BaseLocationId,
                Status = TeamStatus.AVAILABLE
            };

            var createdTeam = await _rescueTeamRepository.CreateAsync(rescueTeam);
            if(!createdTeam) throw new Exception("Lỗi hệ thống: Không thể tạo đội cứu hộ");

            var currentRoles = await _userRepository.GetUserRolesAsync(request.TeamLeaderId);
            var rolesList = currentRoles.ToList();
            if (rolesList.Contains("RescuerLeader")) throw new Exception("Người này đã là Chỉ huy của một đội cứu hộ khác.");


            if (rolesList.Contains("Rescuer")) rolesList.Remove("Rescuer");
            rolesList.Add("RescuerLeader");

            var leaderAdded = await _rescueTeamRepository.AddMemberAsync(teamId, request.TeamLeaderId);
            if(!leaderAdded) throw new Exception("Tạo đội thành công nhưng không thể gán Đội trưởng vào biên chế.");

            await _userRepository.UpdateUserRolesAsync(request.TeamLeaderId, rolesList);

            if (request.MemberIds != null && request.MemberIds.Any())
            {
                foreach (var memberId in request.MemberIds)
                {
                    if (memberId == request.TeamLeaderId) continue; 

                    var isMemberAdded = await _rescueTeamRepository.AddMemberAsync(teamId, memberId);
                    if (isMemberAdded)
                    {
                        var memberRoles = (await _userRepository.GetUserRolesAsync(memberId)).ToList();
                        if (!memberRoles.Contains("Rescuer"))
                        {
                            memberRoles.Add("Rescuer");
                            await _userRepository.UpdateUserRolesAsync(memberId, memberRoles);
                        }
                    }
                }
            }
            return new RescueTeamDTO
            {
                Id = rescueTeam.Id,
                TeamName = rescueTeam.TeamName,
                Status = rescueTeam.Status.ToString()
            };
        }
    }
}