using Kindergarten.Application.UseCase.Admins.Queries.AttendenceQueries;
using Kindergarten.Application.UseCase.Teachers.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Kindergarten.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendenceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AttendenceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAttendenceChildAsync(CreateAttendenceTimeCommand command)
        {
           var response =await _mediator.Send(command);

            return Ok(response);
        }

        [HttpGet("data/child")]
        public async Task<IActionResult> GetAttendenceChildAsync([FromQuery] GetAttendenceDataQuery query)
        {
            var response = await _mediator.Send(query);

            return Ok(response);
        }

        [HttpGet("data/all/child")]
        public async Task<IActionResult> GetAllAttendenceChildAsync([FromQuery] GetAllAttendenceDataQuery query)
        {
            var response = await _mediator.Send(query);

            return Ok(response);
        }
    }
}
