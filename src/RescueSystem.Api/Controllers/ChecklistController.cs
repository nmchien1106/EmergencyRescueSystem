using MediatR;
using Microsoft.AspNetCore.Mvc;
using RescueSystem.Application.Features.Checklist.Commands.CreateChecklist;
using RescueSystem.Application.Features.Checklist.Queries.GetAllChecklists;
using RescueSystem.Application.Features.Checklist.Queries.GetChecklistDetail;
using RescueSystem.Application.Features.ChecklistItem.Commands.CreateChecklistItem;
using RescueSystem.Application.Features.ChecklistItem.Commands.UpdateChecklistItem;

namespace RescueSystem.Api.Controllers
{
    [ApiController]
    [Route("api/checklist")]
    public class ChecklistController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ChecklistController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST api/checklist
        [HttpPost]
        public async Task<IActionResult> Create(CreateChecklistCommand command)
        {
            var id = await _mediator.Send(command);

            return Ok(new
            {
                success = true,
                statusCode = 200,
                message = "Checklist created successfully.",
                id = id
            });
        }

        // GET api/checklist
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllChecklistsQuery());

            return Ok(new
            {
                success = true,
                statusCode = 200,
                message = "Checklists retrieved successfully.",
                data = result
            });
        }

        // GET api/checklist/{id} - join checklist items
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(
                new GetChecklistDetailQuery
                {
                    Id = id
                });

            return Ok(new
            {
                success = true,
                statusCode = 200,
                message = "Checklist detail retrieved successfully.",
                data = result
            });
        }

        // POST /api/checklist/{checklistId}/items
        [HttpPost("{checklistId}/items")]
        public async Task<IActionResult> CreateItem(Guid checklistId, CreateChecklistItemCommand command)
        {
            command.ChecklistId = checklistId;

            var result = await _mediator.Send(command);

            return Ok(new
            {
                success = true,
                statusCode = 200,
                message = "Tạo thành công checklist item",
                id = result
            });
        }

        //PUT /api/checklist/items/{id}
        [HttpPut("items/{id}")]
        public async Task<IActionResult> UpdateItem(Guid id, UpdateChecklistItemCommand command)
        {
            command.Id = id;

            await _mediator.Send(command);

            return NoContent();
        }
    }
}
