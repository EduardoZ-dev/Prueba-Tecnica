using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Commands.CustomFields.Add;
using Application.Commands.CustomFields.Update;
using Application.Commands.CustomFields.Delete;
using Domain.Exceptions;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/providers/{providerId}/custom-fields")]
    public class CustomFieldController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomFieldController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromRoute] Guid providerId, [FromBody] AddCustomFieldToProviderCommand command, CancellationToken cancellationToken)
        {
            if (providerId != command.ProviderId)
                return BadRequest("Provider ID mismatch");
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

        [HttpPut("{customFieldId}")]
        public async Task<IActionResult> Update([FromRoute] Guid providerId, [FromRoute] Guid customFieldId, [FromBody] UpdateCustomFieldCommand command, CancellationToken cancellationToken)
        {
            if (providerId != command.ProviderId || customFieldId != command.CustomFieldId)
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

        [HttpDelete("{customFieldId}")]
        public async Task<IActionResult> Delete([FromRoute] Guid providerId, [FromRoute] Guid customFieldId, CancellationToken cancellationToken)
        {
            try
            {
                var command = new DeleteCustomFieldCommand(providerId, customFieldId);
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