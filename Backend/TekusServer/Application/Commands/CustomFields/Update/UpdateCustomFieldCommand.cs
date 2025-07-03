using Application.Abstractions;

namespace Application.Commands.CustomFields.Update
{
    public sealed record UpdateCustomFieldCommand(
        Guid ProviderId,
        Guid CustomFieldId,
        string NewValue
    ) : ICommand;
}
