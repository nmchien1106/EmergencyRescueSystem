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

namespace RescueSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MissionController(IMediator mediator) : ControllerBase
    {
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
            // 1. Gửi command đi xử lý
            var res = await mediator.Send(command);

            // 2. Trả về format ApiResponse chuẩn của dự án
            return StatusCode(201,
                ApiResponse<object>.SuccessResponse(
                    new { Id = res }, // Trả về ID của Mission mới tạo
                    "Create mission successfully",
                    StatusCodes.Status201Created
                )
            );
        }

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
                    res,
                    "Get mission details successfully",
                    StatusCodes.Status200OK
                )
            );
        }

        [HttpGet]
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
                    res,
                    "Get missions successfully",
                    StatusCodes.Status200OK
                )
            );
        }

        [Authorize(Roles = "Rescuer,Dispatcher")]
        [HttpPut("{id}/status")]
        [SwaggerOperation(
            Summary = "Update mission status",
            Description = "Cập nhật trạng thái nhiệm vụ"
        )]
        public async Task<ActionResult<object>> UpdateMissionStatus(Guid id, [FromBody] UpdateMissionCommand command)
        {
            command.MissionId = id;
            var res = await mediator.Send(command);

            if (!res)
            {
                return BadRequest("Không thể cập nhật trạng thái");
            }

            return Ok(
                ApiResponse<object>.SuccessResponse(
                    null,
                    "Update mission successfully",
                    StatusCodes.Status200OK
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
                return BadRequest("Không thể hoàn thành nhiệm vụ");
            }

            return Ok(
                ApiResponse<object>.SuccessResponse(
                    null,
                    "Finish mission successfully",
                    StatusCodes.Status200OK
                )
            );
        }
    }
}


