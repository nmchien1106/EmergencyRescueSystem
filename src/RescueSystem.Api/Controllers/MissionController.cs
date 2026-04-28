using MediatR;
using Microsoft.AspNetCore.Mvc;
using RescueSystem.Application.Common.Response;
using RescueSystem.Application.DTOs.Mission;
using RescueSystem.Application.Features.Missions.Commands.CreateMission;
using RescueSystem.Application.Features.Missions.Queries.GetMissionById;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using RescueSystem.Application.Features.Missions.Commands.UpdateMission;
using RescueSystem.Application.Features.Missions.Commands.FinishMission;
using RescueSystem.Application.Features.Missions.Queries.GetMissionsWithPagination;
using RescueSystem.Domain.Entities;
using RescueSystem.Application.Common.Exception;

namespace RescueSystem.Api.Controllers
{
    [ApiController]
    [Route("api/missions")]
    public class MissionController(IMediator mediator) : ControllerBase
    {
        // POST api/missions
        [HttpPost]
        [SwaggerOperation(
        Summary = "Create a new rescue mission",
        Description = "Dispatcher phân công một đội cứu hộ cho một yêu cầu cứu hộ cụ thể"
        )]
        [SwaggerResponse(201, "Mission created successfully", typeof(ApiResponse<Guid>))]
        [SwaggerResponse(400, "Invalid input data")]
        [SwaggerResponse(500, "Internal server error")]
        public async Task<ActionResult<object>> CreateMission([FromBody] CreateMissionCommand command)
        {

            var res = await mediator.Send(command);

            return StatusCode(201,
                ApiResponse<object>.SuccessResponse(
                    data: new { Id = res },
                    message: "Create mission successfully",
                    statusCode: StatusCodes.Status201Created
                )
            );
        }

        // GET api/missions/{id}
        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Get mission details by ID",
            Description = "Lấy thông tin chi tiết của 1 nv bằng ID"
            )]
        [SwaggerResponse(200, "Mission details retrieved successfully", typeof(ApiResponse<MissionDTO>))]
        [SwaggerResponse(404, "Mission not found")]
        [SwaggerResponse(500, "Internal server error")]
        public async Task<ActionResult<object>> GetMissionById([FromRoute] Guid id)
        {
            var res = await mediator.Send(new GetMissionByIdQuery { Id = id });
            return Ok(
                ApiResponse<MissionDTO>.SuccessResponse(
                    data: res,
                    message: "Get mission details successfully",
                    statusCode: StatusCodes.Status200OK
                )
            );
        }

        // GET api/missions
        [HttpGet]
        [Authorize(Roles = "Dispatcher")]
        [SwaggerOperation(
            Summary = "Get missions with pagination",
            Description = "Lấy danh sách nhiệm vụ có phân trang và lọc"
        )]
        [SwaggerResponse(200, "Get missions successfully")]
        public async Task<ActionResult<object>> GetMissions([FromQuery] GetMissionsWithPaginationQuery query)
        {
            var res = await mediator.Send(query);

            return Ok(
                ApiResponse<object>.SuccessResponse(
                    data: res,
                    message: "Get missions successfully",
                    statusCode: StatusCodes.Status200OK
                )
            );
        }

        [Authorize(Roles = "Rescuer,Dispatcher")]
        [HttpPut("{id}/status")]
        [SwaggerOperation(
            Summary = "Update mission status",
            Description = "Cập nhật trạng thái nhiệm vụ"
        )]
        public async Task<ActionResult<object>> UpdateMissionStatus(Guid id, UpdateMissionCommand command)
        {
            command.MissionId = id;
            var res = await mediator.Send(command);

            if (!res)
            {
                throw new BadRequestException("Không thể cập nhật trạng thái nhiệm vụ");
            }

            return Ok(
                ApiResponse<object>.SuccessResponse(
                    data: null,
                    message: "Update mission successfully",
                    statusCode: StatusCodes.Status200OK
                )
            );
        }

        [Authorize(Roles = "Rescuer,Dispatcher")]
        [HttpPut("{id}/finish")]
        [SwaggerOperation(
            Summary = "Finish mission",
            Description = "Đánh dấu nhiệm vụ hoàn thành"
        )]
        public async Task<ActionResult<object>> FinishMission(Guid id)
        {
            var command = new FinishMissionCommand
            {
                MissionId = id
            };

            var res = await mediator.Send(command);

            if (!res)
            {
                throw new BadRequestException("Không thể hoàn thành nhiệm vụ");
            }

            return Ok(
                ApiResponse<object>.SuccessResponse(
                    data: null,
                    message: "Finish mission successfully",
                    statusCode: StatusCodes.Status200OK
                )
            );
        }
    }
}


