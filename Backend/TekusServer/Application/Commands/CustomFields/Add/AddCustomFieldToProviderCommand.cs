using Application.Abstractions;

namespace Application.Commands.CustomFields.Add
{
    public sealed record AddCustomFieldToProviderCommand(
        Guid ProviderId,
        string Key,
        string Value
    ) : ICommand;
}
