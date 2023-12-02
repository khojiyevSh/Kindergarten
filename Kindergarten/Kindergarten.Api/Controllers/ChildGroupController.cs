using Kindergarten.Application.UseCase.Admins.Commands.ChildGroupCommands;
using Kindergarten.Application.UseCase.Admins.Queries.ChildGroupQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Kindergarten.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChildGroupController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ChildGroupController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateChildGroupAsync(CreateChildGroupCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteChildGroupAsync([FromForm] DeleteChildGroupCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateChildGroupAsync(UpdateChildGroupCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetChildGroupAsync([FromQuery] GetChildGroupQuery query)
        {
            var response = await _mediator.Send(query);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllChildGroupAsync([FromQuery] GetAllChildGroupQuery query)
        {
            var response = await _mediator.Send(query);

            return Ok(response);
        }
    }
}
