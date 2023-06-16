using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpellSmarty.Application.Commands;
using SpellSmarty.Application.Queries;

namespace SpellSmarty.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> GetAccounts()
        {
            try
            {
                var accounts = await _mediator.Send(new GetAllUserQuery());
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500);
            }

        }

        [HttpPut]
        public async Task<ActionResult> UpgradePremium([FromBody] UpgradePremiumCommand command)
        {
            try
            {
                var check = await _mediator.Send(command);
                return Ok(check);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500);
            }

        }

        [HttpGet("details")]
        public async Task<IActionResult> GetUserDetais([FromHeader] string? Authorization)
        {
            if (Authorization == null || Authorization.Trim() == "") return BadRequest();
            var userDetails = await _mediator.Send(new GetUserDetailsQuery(Authorization));
            return Ok(userDetails);
        }
    }
}
