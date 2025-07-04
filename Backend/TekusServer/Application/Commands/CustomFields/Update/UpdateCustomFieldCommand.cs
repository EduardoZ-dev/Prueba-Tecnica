using MediatR;

namespace Application.Commands.CustomFields.Update
{
    public sealed record UpdateCustomFieldCommand(
        Guid ProviderId,
        Guid CustomFieldId,
        string NewValue
    ) : IRequest;
}
