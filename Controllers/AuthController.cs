using MediatR;
using Microsoft.AspNetCore.Mvc;
using SpellSmarty.Application.Commands;
using SpellSmarty.Application.Dtos;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SpellSmarty.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        [ProducesDefaultResponseType(typeof(AuthResponseDto))]
        public async Task<IActionResult> Login([FromBody] AuthCommand command)
        {
            var response = await _mediator.Send(command);
            if (!response.Error) return Ok(response);
            else
            {
                var i = new ErrorHandling(response.Exception);
                return i;
            }
        }
        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] SignUpCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpPost("verify")]
        public async Task<IActionResult> Verify([FromBody] VerifyAccountCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpPost("logout")]
        public async Task<IActionResult> Logout(LogoutCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

    }
}
