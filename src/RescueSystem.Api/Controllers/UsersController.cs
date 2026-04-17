using Microsoft.AspNetCore.Mvc;
using RescueSystem.Application.DTOs.User;
using RescueSystem.Application.Common.Response;
using RescueSystem.Domain.Entities;
using MediatR;
using RescueSystem.Application.Features.User.Commands;
using RescueSystem.Application.Features.User.Queries.GetAllUser;
using RescueSystem.Application.Features.User.Queries.GetUserById;
using Swashbuckle.AspNetCore.Annotations;
using RescueSystem.Application.Features.User.Commands.UpdateUser;
using RescueSystem.Application.Features.User.Commands.DeleteUser;


namespace RescueSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(IMediator mediator) : ControllerBase
    {
        // ----------------------- //
        [HttpPost]
        [SwaggerOperation(
            Summary = "Create a new user",
            Description = "Tạo người dùng mới"
        )]
        [SwaggerResponse(201, "User created successfully")]
        [SwaggerResponse(500, "Internal server error")]
        public async Task<ActionResult<object>> CreateUser([FromBody] CreateUserCommand dto)
        {
            var res = await mediator.Send(dto);
            return StatusCode(201, ApiResponse<object>.SuccessResponse(null, "Create user successfully", StatusCodes.Status201Created));
        }

        // ----------------------- //
        [HttpGet]
        [SwaggerOperation(
            Summary = "Get all users",
            Description = "Lấy thông tin tất cả người dùng"
        )]
        [SwaggerResponse(200, "Success", typeof(ApiResponse<UserDTO[]>))]
        [SwaggerResponse(500, "Internal server error")]
        public async Task<ActionResult<object>> GetAllUsers()
        {
            var res = await mediator.Send(new GetAllUserQuery());

            return Ok(ApiResponse<object>.SuccessResponse(res, "Get all users successfully", StatusCodes.Status200OK));
        }

        // ----------------------- //
        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Get user by id",
            Description = "Lấy thông tin user theo Id"
        )]
        [SwaggerResponse(200, "Success", typeof(ApiResponse<UserDTO>))]
        [SwaggerResponse(404, "User not found")]
        [SwaggerResponse(500, "Internal server error")]
        public async Task<ActionResult<UserDTO>> GetUserById([FromRoute] Guid id)
        {
            var result = await mediator.Send(new GetUserByIdQuery { Id = id });
            return Ok(ApiResponse<UserDTO>.SuccessResponse(result, "Get user by id successfully", StatusCodes.Status200OK));
        }
        // ----------------------- //

        //// Update user
        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserCommand command)
        //{
        //    command.Id = id;

        //    await mediator.Send(command);

        //    return NoContent();
        //}
        // Update user
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserCommand command)
        {
            command.Id = id;

            // Mediator sẽ trả về true nếu thành công, false nếu không tìm thấy user
            var result = await mediator.Send(command);

            if (result)
            {
                return Ok(new
                {
                    status = 200,
                    success = true,
                    message = "Cập nhật thông tin người dùng thành công."
                });
            }

            return NotFound(new
            {
                status = 404,
                success = false,
                message = $"Không tìm thấy người dùng có ID: {id}"
            });
        }

        // delete user
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var command = new DeleteUserCommand { Id = id };
            var result = await mediator.Send(command);

            if (result) // Nếu xóa thành công
            {
                return Ok(new
                {
                    status = 200,
                    success = true,
                    message = "Đã xóa người dùng thành công."
                });
            }

            // Nếu không tìm thấy hoặc lỗi
            return NotFound(new
            {
                status = 404,
                success = false,
                message = "Không tìm thấy người dùng để xóa."
            });

        }
    }
}
