namespace Application.DTOs
{
    public sealed record DashboardDto(
        Dictionary<string, int> ServicesPerCountry,
        Dictionary<string, int> ProvidersPerCountry,
        List<TopServiceDto> TopServices,
        Dictionary<string, int> ServicesPerProvider,
        string? CountryWithMostServices
    );

    public sealed record TopServiceDto(string Name, int Count);

}
