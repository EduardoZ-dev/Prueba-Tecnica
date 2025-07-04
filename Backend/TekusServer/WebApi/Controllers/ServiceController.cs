using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Commands.ServicesProvider.Create;
using Application.Commands.ServicesProvider.Update;
using Application.Commands.ServicesProvider.Delete;
using Application.Queries.ServicesProvider.GetAll;
using Application.Queries.ServicesProvider.GetById;
using Domain.Exceptions;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/services")]
    public class ServiceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ServiceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllServicesQuery(), cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var query = new GetServiceByIdQuery(id);
                var result = await _mediator.Send(query, cancellationToken);
                return Ok(result);
            }
            catch (DomainException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpPost("providers/{providerId}/services")]
        public async Task<IActionResult> AddToProvider([FromRoute] Guid providerId, [FromBody] AddServiceToProviderCommand command, CancellationToken cancellationToken)
        {
            if (providerId != command.ProviderId)
                return BadRequest("ProviderId mismatch");

            var id = await _mediator.Send(command, cancellationToken);

            return Created($"api/services/{id}", null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateServiceCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id)
                return BadRequest("ID mismatch");
            try
            {
                await _mediator.Send(command, cancellationToken);
                return Ok();
            }
            catch (DomainException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var command = new DeleteServiceCommand(id);

                await _mediator.Send(command, cancellationToken);

                return NoContent();
            }
            catch (DomainException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }
    }
} 