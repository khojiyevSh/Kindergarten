using Kindergarten.Application.UseCase.UsersAuth.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Kindergarten.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator  _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginUserCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }
    }
}
