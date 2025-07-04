using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Commands.Providers.Create;
using Application.Commands.Providers.UpdateProvider;
using Application.Commands.Providers.DeleteProvider;
using Application.Queries.Providers.GetAll;
using Application.Queries.Providers.GetById;
using Domain.Exceptions;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/providers")]
    public class ProviderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProviderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllProvidersQuery query,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var query = new GetProviderByIdQuery(id);
                var result = await _mediator.Send(query, cancellationToken);
                if (result == null)
                    return NotFound();
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProviderCommand command,
            CancellationToken cancellationToken)
        {
            var id = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateProviderCommand command,
            CancellationToken cancellationToken)
        {
            if (id != command.Id)
                return BadRequest("ID mismatch");
            try
            {
                await _mediator.Send(command, cancellationToken);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (DomainException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var command = new DeleteProviderCommand(id);
                await _mediator.Send(command, cancellationToken);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return new ObjectResult(new { error = ex.Message }) { StatusCode = 404 };
            }
        }
    }
}
