namespace Application.DTOs
{
    public record SummaryDto(
        Dictionary<string, int> ProvidersPerCountry,
        Dictionary<string, int> ServicesPerCountry
    );
}
