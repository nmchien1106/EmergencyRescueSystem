using Microsoft.AspNetCore.Mvc;
using RescueSystem.Application.DTOs.User;
using RescueSystem.Application.Common.Response;
using RescueSystem.Domain.Entities;
using MediatR;
using RescueSystem.Application.Features.User.Commands;
using RescueSystem.Application.Features.User.Queries.GetAllUser;
using RescueSystem.Application.Features.User.Queries.GetUserById;

namespace RescueSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(IMediator mediator) : ControllerBase
    {

        [HttpPost]
        public async Task<ActionResult<object>> CreateUser([FromBody] CreateUserCommand dto)
        {
            var res = await mediator.Send(dto);
            return StatusCode(201, ApiResponse<object>.SuccessResponse(null, "Create user successfully", StatusCodes.Status201Created));
        }
        [HttpGet]
        public async Task<ActionResult<object>> GetAllUsers()
        {
            var res = await mediator.Send(new GetAllUserQuery());

            return Ok(ApiResponse<object>.SuccessResponse(res, "Get all users successfully", StatusCodes.Status200OK));
        }
        // get user by id
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserById([FromRoute] Guid id)
        {
            var result = await mediator.Send(new GetUserByIdQuery { Id = id });
            return Ok(ApiResponse<UserDTO>.SuccessResponse(result, "Get user by id successfully", StatusCodes.Status200OK));
        }
    }
}
