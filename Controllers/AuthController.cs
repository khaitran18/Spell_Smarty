using MediatR;
using Microsoft.AspNetCore.Mvc;
using SpellSmarty.Application.Commands;
using SpellSmarty.Application.Dtos;
using SpellSmarty.Application.Services;
using SpellSmarty.Domain.Models;
using SpellSmarty.Infrastructure.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SpellSmarty.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMailService _mail;

        public AuthController(IMediator mediator,IMailService mail)
        {
            _mediator = mediator;
            _mail= mail;
        }

        [HttpPost("login")]
        [ProducesDefaultResponseType(typeof(AuthResponseDto))]
        public async Task<IActionResult> Login([FromBody] AuthCommand command)
        {
            return Ok(await _mediator.Send(command));
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

        //[HttpPost("sendmail")]
        //public async Task<IActionResult> SendMailAsync(MailDataModel mailData)
        //{
        //    bool result = await _mail.SendAsync(mailData, new CancellationToken());

        //    if (result)
        //    {
        //        return StatusCode(StatusCodes.Status200OK, "Mail has successfully been sent.");
        //    }
        //    else
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "An error occured. The Mail could not be sent.");
        //    }
        //}
    }
}
