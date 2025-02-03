using FraudSys.Domain.Commands.v1.ProcessPixTransaction;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FraudSys.Api.Controllers
{
    [ApiController]
    [Route("api/v1/pix/")]
    public class PixTransactionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PixTransactionController(
            IMediator mediator
        )
        {
            _mediator = mediator;
        }

        [HttpPost("transaction")]
        public async Task<IActionResult> ProcessPixTransaction([FromBody] ProcessPixTransactionCommand command)
        {
            if (command == null)
                return BadRequest("Invalid request.");

            if (command.TransactionAmount <= 0)
                return BadRequest("The transaction amount must be greater than zero.");

            bool isApproved = await _mediator.Send(command);

            if (isApproved)
                return Ok(new { message = "Transaction approved." });
            else
                return BadRequest(new { message = "Transaction denied. Insufficient limit." });
        }
    }
}