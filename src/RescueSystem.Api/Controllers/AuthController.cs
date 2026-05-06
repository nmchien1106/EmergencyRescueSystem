using CloudinaryDotNet.Actions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RescueSystem.Application.Common.Exception;
using RescueSystem.Application.Common.Interfaces.Services;
using RescueSystem.Application.Common.Response;
using RescueSystem.Application.DTOs.Auth;
using RescueSystem.Application.Features.Auth.Commands.ForgotPassword;
using RescueSystem.Application.Features.Auth.Commands.Login;
using RescueSystem.Application.Features.Auth.Commands.Register;
using RescueSystem.Application.Features.Auth.Commands.ResetPassword;
using RescueSystem.Application.Features.Auth.Commands.UpdateAvatar;
using RescueSystem.Application.Features.Auth.Commands.UpdateProfile;
using RescueSystem.Application.Features.Auth.Queries.Profile;
using Swashbuckle.AspNetCore.Annotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RescueSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IMediator _mediator;
        private readonly IEmailService _emailService; 
        public AuthController(IMediator mediator, IEmailService emailService)
        {
            _mediator = mediator;
            _emailService = emailService;  
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

        // Post api/auth/forgot-password
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(new
            {
                success = true,
                statusCode = 200,
                message = result
            });
        }

        // Post api/auth/reset-password
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(new
            {
                success = true,
                statusCode = 200,
                message = result
            });
        }

        // Put api/auth/profile
        [Authorize]
        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileCommand command)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            if (userId == null)
            {
                throw new UnauthorizedException("Lỗi xác thực người dùng");
            }

            var result = await _mediator.Send(command);
            return Ok(new
            {
                success = true,
                statusCode = 200,
                message = result
            });
        }

        //  Post api/auth/avatar
        [Authorize]
        [HttpPost("avatar")]
        public async Task<IActionResult> UploadAvatar([FromForm] UpdateAvatarCommand command)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            if (userId == null)
            {
                throw new UnauthorizedException("Lỗi xác thực người dùng");
            }

            var result = await _mediator.Send(command);

            return Ok(new
            {
                success = true,
                statusCode = 200,
                message = "Upload avatar success",
                data = result
            });
        }
    }
}
