using MediatR;

namespace Application.Commands.CustomFields.Delete
{
    public sealed record DeleteCustomFieldCommand(Guid ProviderId, Guid CustomFieldId) : IRequest;
}
