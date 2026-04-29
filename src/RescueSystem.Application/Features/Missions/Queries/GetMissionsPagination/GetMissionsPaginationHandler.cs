using MediatR;
using Microsoft.EntityFrameworkCore;
using RescueSystem.Application.Common.Interfaces.Repositories;
using RescueSystem.Application.Common.Models;
using RescueSystem.Application.DTOs.Mission;
using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.Features.Missions.Queries.GetMissionsPagination
{
    public class GetMissionsPaginationHandler : IRequestHandler<GetMissionsPaginationQuery, PagedResult<MissionDTO>>
    {
        private readonly IMissionRepository _missionRepository;
        public GetMissionsPaginationHandler(IMissionRepository missionRepository)
        {
            _missionRepository = missionRepository;
        }

        public async Task<PagedResult<MissionDTO>> Handle(GetMissionsPaginationQuery request, CancellationToken cancellationToken)
        {

            // Gọi repository để lấy dữ liệu với phân trang và lọc
            var query = _missionRepository.Query();

            // filter trạng thái nếu có
            if (request.Status.HasValue)
            {
                query = query.Where(x => x.Status == request.Status.Value);
            }

            // filter đội cứu hộ nếu có
            if (request.RescueTeamId.HasValue)
            {
                query = query.Where(x => x.RescueTeamId == request.RescueTeamId.Value);
            }

            var page = request.Page <= 0 ? 1 : request.Page;
            var pageSize = request.PageSize <= 0 ? 10 : request.PageSize;
            // tong so banr ghi
            var totalCount = await query.CountAsync(cancellationToken);
            // phan trang
            var data = await query
                .OrderByDescending(x => x.StartTime)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(m => new MissionDTO
                {
                    Id = m.Id,
                    RequestId = m.RequestId,
                    Description = m.Request != null ? m.Request.Description : null,
                    DispatcherId = m.DispatcherId,
                    DispatcherName = m.Dispatcher != null ? m.Dispatcher.FullName : null,
                    RescueTeamId = m.RescueTeamId,
                    TeamName = m.RescueTeam != null ? m.RescueTeam.TeamName : null,
                    StartTime = m.StartTime.AddHours(7),
                    EndTime = m.EndTime.HasValue ? m.EndTime.Value.AddHours(7) : null,
                    CreateAt = m.CreatedAt.AddHours(7),
                    UpdateAt = m.UpdatedAt.AddHours(7),
                    Status = m.Status.ToString()
                })
                .ToListAsync(cancellationToken);
            return new PagedResult<MissionDTO>
            {
                Items = data,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
            };
        }
    }
}
