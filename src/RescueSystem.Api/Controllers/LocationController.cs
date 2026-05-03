using MediatR;
using Microsoft.AspNetCore.Mvc;
using RescueSystem.Application.Common.Response;
using RescueSystem.Application.DTOs.Location;
using RescueSystem.Application.Features.Location.Commands.CreateLocation;
using RescueSystem.Application.Features.Location.Commands.DeleteLocation;
using RescueSystem.Application.Features.Location.Commands.UpdateLocation;
using RescueSystem.Application.Features.Location.Queries.GetAllLocations;
using RescueSystem.Application.Features.Location.Queries.GetLocationById;
using Swashbuckle.AspNetCore.Annotations;

namespace RescueSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [SwaggerOperation(Summary = "Get all locations", Description = "Lấy danh sách tất cả vị trí")]
        [SwaggerResponse(200, "Success", typeof(ApiResponse<List<LocationDTO>>))]
        public async Task<ActionResult<object>> GetAllLocations()
        {
            var result = await mediator.Send(new GetAllLocationsQuery());
            return Ok(ApiResponse<object>.SuccessResponse(result, "Lấy danh sách vị trí thành công", StatusCodes.Status200OK));
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get location by id", Description = "Lấy chi tiết vị trí theo Id")]
        [SwaggerResponse(200, "Success", typeof(ApiResponse<LocationDTO>))]
        [SwaggerResponse(404, "Location not found")]
        public async Task<ActionResult<object>> GetLocationById([FromRoute] Guid id)
        {
            var result = await mediator.Send(new GetLocationByIdQuery { Id = id });
            return Ok(ApiResponse<LocationDTO>.SuccessResponse(result, "Lấy vị trí thành công", StatusCodes.Status200OK));
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create location", Description = "Tạo mới vị trí")]
        [SwaggerResponse(201, "Created", typeof(ApiResponse<LocationDTO>))]
        public async Task<ActionResult<object>> CreateLocation([FromBody] CreateLocationCommand command)
        {
            var result = await mediator.Send(command);
            return StatusCode(StatusCodes.Status201Created, ApiResponse<LocationDTO>.SuccessResponse(result, "Tạo vị trí thành công", StatusCodes.Status201Created));
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update location", Description = "Cập nhật vị trí")]
        [SwaggerResponse(200, "Success", typeof(ApiResponse<LocationDTO>))]
        public async Task<ActionResult<object>> UpdateLocation([FromRoute] Guid id, [FromBody] UpdateLocationCommand command)
        {
            command.Id = id;
            var result = await mediator.Send(command);
            return Ok(ApiResponse<LocationDTO>.SuccessResponse(result, "Cập nhật vị trí thành công", StatusCodes.Status200OK));
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete location", Description = "Xóa vị trí")]
        [SwaggerResponse(200, "Success")]
        public async Task<ActionResult<object>> DeleteLocation([FromRoute] Guid id)
        {
            var result = await mediator.Send(new DeleteLocationCommand { Id = id });
            return Ok(ApiResponse<object>.SuccessResponse(new { Deleted = result }, "Xóa vị trí thành công", StatusCodes.Status200OK));
        }
    }
}
