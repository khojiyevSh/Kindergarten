using Kindergarten.Application.UseCase.Admins.Commands.ChildernCommands;
using Kindergarten.Application.UseCase.Admins.Queries.ChildernQueries;
using Kindergarten.Application.UseCase.Childerns.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kindergarten.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("AdminActions")]
    public class ChildernController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ChildernController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateChildAsync(CreateChildernCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response); 
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetChildernAsync([FromQuery] GetChildernQuery command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllChildernAsync([FromQuery] GetAllChildernQuery command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpGet("noactive")]
        public async Task<IActionResult> GetAllNoActiveChildernAsync([FromQuery] GetAllNoActiveChildernQuery command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpGet("noactive/id")]
        public async Task<IActionResult> GetNoActiveChildernAsync([FromQuery] GetNoActiveChildernQuery command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateChildernAsync(UpdateChildernCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [Authorize("ChildernActions")]
        [HttpPut("himselp")]
        public async Task<IActionResult> UpdateChildernAsync(UpdateChildernHimselfCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteChildernAsync([FromForm] DeleteChildernCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }
    }
}
