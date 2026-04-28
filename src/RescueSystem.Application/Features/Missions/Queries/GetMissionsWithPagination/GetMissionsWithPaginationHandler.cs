using MediatR;
using Microsoft.EntityFrameworkCore;
using RescueSystem.Application.Common.Interfaces.Repositories;
using RescueSystem.Application.Common.Models;
using RescueSystem.Application.DTOs.Mission;
using System;
using System.Collections.Generic;
using System.Text;

namespace RescueSystem.Application.Features.Missions.Queries.GetMissionsWithPagination
{
    public class GetMissionsWithPaginationHandler : IRequestHandler<GetMissionsWithPaginationQuery, PagedResult<MissionDTO>>
    {
        private readonly IMissionRepository _missionRepository;
        public GetMissionsWithPaginationHandler(IMissionRepository missionRepository)
        {
            _missionRepository = missionRepository;
        }

        public async Task<PagedResult<MissionDTO>> Handle(GetMissionsWithPaginationQuery request, CancellationToken cancellationToken)
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
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(m => new MissionDTO
                {
                    Id = m.Id,
                    RequestId = m.RequestId,
                    DispatcherId = m.DispatcherId,
                    RescueTeamId = m.RescueTeamId,
                    StartTime = m.StartTime,
                    EndTime = m.EndTime,
                    Status = m.Status.ToString(),
                    TeamName = m.RescueTeam != null ? m.RescueTeam.TeamName : null
                })
                .ToListAsync(cancellationToken);
            return new PagedResult<MissionDTO>
            {
                Items = data,
                TotalCount = totalCount
            };
        }
    }
}
