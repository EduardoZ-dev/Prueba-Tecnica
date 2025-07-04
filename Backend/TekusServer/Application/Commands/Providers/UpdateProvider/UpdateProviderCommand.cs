using Application.Commands.Providers.Create;
using MediatR;

namespace Application.Commands.Providers.UpdateProvider
{
    public sealed record UpdateProviderCommand(
        Guid Id,
        string Name,
        string Email,
        List<CustomFieldDto> CustomFields,
        List<ServiceDto> Services
    ) : IRequest;


}