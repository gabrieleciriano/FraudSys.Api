using FraudSys.Domain.Commands.v1.CreateAccountLimit;
using FraudSys.Domain.Commands.v1.UpdateAccountLimit;
using FraudSys.Domain.Queries.v1.GetAccountLimit;
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

        [HttpGet("get")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAccountLimit([FromBody] GetAccountLimitQuery query)
        {
            if (query == null) return NotFound();

            var response = await _mediator.Send(query);

            return Ok(response);
        }

        [HttpPut("update")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateAccountLimit([FromBody] UpdateAccountLimitCommand command)
        {
            if (command == null) return NotFound();

             await _mediator.Send(command);

            return Ok(new { Message = "Limit successfully updated. Please check your new limit." });
        }
    }
}