using Kindergarten.Application.UseCase.Admins.Commands.GroupCommands;
using Kindergarten.Application.UseCase.Admins.Queries.GroupQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Kindergarten.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GroupController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateGroupAsync(CreateGroupCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteGroupAsync([FromForm] DeleteGroupCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGroupAsync(UpdateGroupCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetGroupAsync([FromQuery] GetGroupQuery query)
        {
            var response = await _mediator.Send(query);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGroupAsync([FromQuery] GetAllGroupQuery query)
        {
            var response = await _mediator.Send(query);

            return Ok(response);
        }

        [HttpGet("noactive")]
        public async Task<IActionResult> GetAllNoActiveGroupAsync([FromQuery] GetAllNoActiveGroupQuery query)
        {
            var response = await _mediator.Send(query);

            return Ok(response);
        }

        [HttpGet("noactive/name")]
        public async Task<IActionResult> GetNoActiveGroupAsync([FromQuery] GetNoActiveGroupQuery query)
        {
            var response = await _mediator.Send(query);

            return Ok(response);
        }
    }
}
