using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RescueSystem.Application.Common.Response;
using RescueSystem.Application.Features.Request.Commands.CreateRequest;

namespace RescueSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private IMediator mediator;
        public RequestController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRequest([FromBody] CreateRequestCommand command)
        {
            var result = await mediator.Send(command);
            return StatusCode(201, ApiResponse<object>.SuccessResponse(
                data: new
                {
                    Id = result.Id
                },
                message: "Tạo yêu cầu cứu hộ thành công",
                statusCode: 201
            ));
        }
    }
}
