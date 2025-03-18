using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Queue_WebApi.CQRS.Groups.Commands;
using Queue_WebApi.CQRS.Groups.Queries;
using Queue_WebApi.CQRS.Halls.Commands;

namespace Queue_WebApi.Controllers
{
    [Authorize(Roles = "admin")]
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
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllGroups()
        {
            var groups = await _mediator.Send(new GetGroups());
            return Ok(groups);
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroupById(Guid id)
        {
            var group = await _mediator.Send(new GetGroupById(id));
            return Ok(group);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateGroup([FromBody] UpdateGroupCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup(Guid id)
        {
            await _mediator.Send(new DeleteGroupCommand(id));
            return NoContent();
        }
    }
}
