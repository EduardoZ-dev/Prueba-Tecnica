namespace Application.DTOs
{
    public sealed record ServiceDto(
        string Name, decimal HourlyRateUsd,
        List<string> Countries);
}
