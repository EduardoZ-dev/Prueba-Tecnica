namespace Application.DTOs
{
    public sealed record ProviderDto(
        Guid Id,
        string Nit,
        string Name,
        string Email,
        List<CustomFieldDto> CustomFields,
        List<ServiceDto> Services
    );

    
}
