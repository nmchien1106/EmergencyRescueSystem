using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RescueSystem.Application.Common.Response;
using RescueSystem.Application.DTOs.Commander;
using RescueSystem.Application.DTOs.User;
using RescueSystem.Application.Features.Commander.Commands.ApproveUser;
using RescueSystem.Application.Features.Commander.Commands.RejectUser;
using RescueSystem.Application.Features.Commander.Commands.ToggleUserStatus;
using RescueSystem.Application.Features.Commander.Queries.GetPendingApprovalUsers;
using RescueSystem.Application.Features.Commander.Queries.GetRejectedUsers;
using RescueSystem.Application.Features.User.Queries.GetSystemUsers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RescueSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Commander")] 
    public class CommanderController(IMediator mediator): ControllerBase
    {
        [HttpGet("approvals/pending")]
        public async Task<ActionResult<ApiResponse<List<UserSystemDTO>>>> GetPendingApprovals()
        {
            var query = new GetPendingApprovalUsersQuery();
            var result = await mediator.Send(query);
            if(result.Count()== 0)
            {
                return Ok(ApiResponse<List<UserSystemDTO>>.SuccessResponse(null, "Tất cả tài khoản đã được phê duyệt", 200));
            }
            return Ok(ApiResponse<List<UserSystemDTO>>.SuccessResponse(result, "Success", StatusCodes.Status200OK));
        }

        [HttpGet("approvals/rejected")]
        public async Task<ActionResult<ApiResponse<List<UserSystemDTO>>>> GetRejected()
        {
            var query = new GetRejectedUsersQuery();
            var result = await mediator.Send(query);
            if(result.Count()== 0)
            {
                return Ok(ApiResponse<List<UserSystemDTO>>.SuccessResponse(null, "Không có tài khoản nào bị từ chối", 200));
            }
            return Ok(ApiResponse<List<UserSystemDTO>>.SuccessResponse(result, "Success", StatusCodes.Status200OK));
        }

        [HttpGet("users")]
        public async Task<ActionResult<ApiResponse<List<UserSystemDTO>>>> GetSystemUsers([FromQuery] string? search, [FromQuery] string? role)
        {
            var query = new GetSystemUsersQuery { Search = search, Role = role };
            var result = await mediator.Send(query);
            return Ok(ApiResponse<List<UserSystemDTO>>.SuccessResponse(result, "Success", StatusCodes.Status200OK));
        }

        // ==========================================
        // 2. CÁC API THAO TÁC (POST / PUT)
        // ==========================================

        [HttpPost("approvals/{userId}")]
        public async Task<ActionResult<ApiResponse<object>>> ApproveUser([FromRoute] Guid userId)
        {
            var command = new ApproveUserCommand { UserId = userId };
            await mediator.Send(command);

            return Ok(ApiResponse<object>.SuccessResponse(null, "Đã phê duyệt tài khoản thành công", 200));
        }

        [HttpPost("approvals/{userId}/reject")]
        public async Task<ActionResult<ApiResponse<object>>> RejectUser([FromRoute] Guid userId)
        {
            var command = new RejectUserCommand { UserId = userId };
            await mediator.Send(command);
            
            return Ok(ApiResponse<object>.SuccessResponse(null, "Đã từ chối tài khoản", 200));
        }

        [HttpPut("users/{userId}/status")]
        public async Task<ActionResult<ApiResponse<object>>> ToggleUserStatus(
            [FromRoute] Guid userId, 
            [FromBody] ToggleStatusRequestDto request)
        {
            var command = new ToggleUserStatusCommand 
            { 
                UserId = userId, 
                IsActive = request.IsActive 
            };
            
            await mediator.Send(command);

            return Ok(ApiResponse<object>.SuccessResponse(null, "Cập nhật trạng thái tài khoản thành công", 200));
        }
    }

    public class ToggleStatusRequestDto
    {
        public bool IsActive { get; set; }
    }
}