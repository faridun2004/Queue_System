using MediatR;
using Microsoft.AspNetCore.Mvc;
using Queue_WebApi.CQRS.Groups.Commands;
using Queue_WebApi.CQRS.Groups.Queries;

namespace Queue_WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupsController: ControllerBase
    {
        private readonly IMediator _mediator;
        public GroupsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateGroup([FromBody] CreateGroupCommand command)
        {
            var groupId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetGroupById), new { id = groupId }, groupId);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllGroups()
        {
            var groups = await _mediator.Send(new GetGroups());
            return Ok(groups);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroupById(Guid id)
        {
            var group = await _mediator.Send(new GetGroupById(id));
            return Ok(group);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGroup([FromBody] UpdateGroupCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup(Guid id)
        {
            await _mediator.Send(new DeleteGroupCommand(id));
            return NoContent();
        }
    }
}
