using Kindergarten.Application.UseCase.Admins.Commands.TeacherCommands;
using Kindergarten.Application.UseCase.Admins.Queries.TeacherQueries;
using Kindergarten.Application.UseCase.Teachers.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kindergarten.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TeacherController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize("AdminActions")]
        [HttpPost]
        public async Task<IActionResult> CreateTeacherAsync(CreateTeacherCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [Authorize("AdminActions")]
        [HttpPut]
        public async Task<IActionResult> UpdateTeacherAsync(UpdateTeacherCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [Authorize("AdminActions")]
        [Authorize("TeacherActions")]
        [HttpPut("himself")]
        public async Task<IActionResult> UpdateHimselfTeacherAsync(UpdateTeacherHimselfCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [Authorize("AdminActions")]
        [HttpDelete]
        public async Task<IActionResult> DeleteTeacherAsync([FromForm] DeleteTeacherCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [Authorize("AdminActions")]
        [HttpGet("id")]
        public async Task<IActionResult> GetTeacherAsync([FromQuery] GetTeacherQuery command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [Authorize("AdminActions")]
        [HttpGet]
        public async Task<IActionResult> GetAllTeacherAsync([FromQuery] GetAllTeacherQuery command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [Authorize("AdminActions")]
        [HttpGet("noactive/id")]
        public async Task<IActionResult> GetNoActiveTeacherAsync([FromQuery] GetNoActiveTeacherQuery command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [Authorize("AdminActions")]
        [HttpGet("noactive")]
        public async Task<IActionResult> GetAllNoActiveTeacherAsync([FromQuery] GetAllNoActiveTeacherQuery command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }
    }
}
