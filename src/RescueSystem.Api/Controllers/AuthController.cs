using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RescueSystem.Application.Features.Auth.Commands.Login;
using RescueSystem.Application.Features.Auth.Commands.Register;
using Swashbuckle.AspNetCore.Annotations;
using RescueSystem.Application.Common.Exception;



using RescueSystem.Application.Features.Auth.Queries.Profile;
using RescueSystem.Application.Common.Response;
using RescueSystem.Application.DTOs.Auth;

namespace RescueSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Post api/auth/register
        [HttpPost("register")]
        [SwaggerOperation(Summary = "Đăng ký tài khoản")]
        public async Task<ActionResult<object>> Register([FromBody] RegisterCommand command)
        {
            var result = await _mediator.Send(command);
            return StatusCode(201, ApiResponse<object>.SuccessResponse(null, "Đăng ký tài khoản thành công", StatusCodes.Status201Created));
        }

        // Post api/auth/login
        [HttpPost("login")]
        [SwaggerOperation(Summary = "Đăng nhập tài khoản")]
        public async Task<ActionResult<object>> Login([FromBody] LoginCommand command)
        {
            var result = await _mediator.Send(command);
            return ApiResponse<AuthResponse>.SuccessResponse(result, "Đăng nhập thành công");
        }

        // Get api/auth/profile
        [Authorize]
        [HttpGet("profile")]
        [SwaggerOperation(Summary = "Lấy thông tin người dùng")]
        public async Task<ActionResult<object>> Profile()
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            if (userId == null)
            {
                throw new UnauthorizedException("Lỗi xác thực người dùng");
            }

            var query = new ProfileQuery
            {
                UserId = userId,
            };
            var response = await _mediator.Send(query);
            return ApiResponse<ProfileResponse>.SuccessResponse(response, "Lấy thông tin người dùng thành công");
        }
    }
}
