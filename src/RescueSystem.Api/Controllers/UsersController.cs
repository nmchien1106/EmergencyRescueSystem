using Microsoft.AspNetCore.Mvc;
using RescueSystem.Application.DTOs.User;
using RescueSystem.Application.Common.Response;
using RescueSystem.Domain.Entities;
using MediatR;
using RescueSystem.Application.Features.User.Commands;

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
    }
}
