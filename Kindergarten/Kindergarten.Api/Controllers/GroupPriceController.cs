using Kindergarten.Application.UseCase.Admins.Commands.GroupPrices;
using Kindergarten.Application.UseCase.Admins.Queries.GroupPriceQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Kindergarten.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupPriceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GroupPriceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateGroupAsync(CreateGroupPriceMonthCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteGroupAsync([FromForm] DeleteGroupPriceMonthCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGroupAsync(UpdateGroupPriceMonthCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetGroupAsync([FromQuery] GetGroupPriceQuery query)
        {
            var response = await _mediator.Send(query);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGroupAsync([FromQuery] GetAllGroupPriceQuery query)
        {
            var response = await _mediator.Send(query);

            return Ok(response);
        }

        [HttpGet("groupId")]
        public async Task<IActionResult> GetAllNoActiveGroupAsync([FromQuery] GetAllGroupPriceGroupIdQuery query)
        {
            var response = await _mediator.Send(query);

            return Ok(response);
        }
    }
}
