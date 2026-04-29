using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RescueSystem.Application.Common.Response;
using RescueSystem.Application.DTOs.Request;
using RescueSystem.Application.Features.Request.Commands.CreateRequest;
using RescueSystem.Application.Features.Request.Queries.GetRequestById;

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

        // POST api/request - Create a new rescue request
        [HttpPost]
        public async Task<IActionResult> CreateRequest([FromForm] CreateRequestCommand command)
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

        // GET api/request/{id} - Get a rescue request by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRequestById(Guid id)
        {
            var query = new GetRequestByIdQuery { RequestId = id };
            var result = await mediator.Send(query);
            return Ok(ApiResponse<RequestDTO>.SuccessResponse(
                data: result,
                message: "Lấy yêu cầu cứu hộ thành công",
                statusCode: 200
            ));
        }
    }
}
