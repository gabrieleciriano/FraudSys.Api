using FraudSys.Domain.Commands.v1.CreateAccountLimit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FraudSys.Api.Controllers
{
    [ApiController]
    [Route("api/v1/limits/")]
    public class AccountLimitController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountLimitController(
            IMediator mediator
        )
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateAccountLimit([FromBody] CreateAccountLimitCommand command)
        {
            bool success = await _mediator.Send(command);

            if (!success) return BadRequest("It was not possible to save the limit");

            return Ok(new { Message = "Limit successfully registered" });
        }
    }
}