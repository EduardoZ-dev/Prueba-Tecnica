namespace Application.DTOs
{
    public sealed record SummaryDto(
        Dictionary<string, int> ProvidersPerCountry,
        Dictionary<string, int> ServicesPerCountry
    );
}
