using Application.Abstractions.Persistence;

namespace Infrastructure.Services
{
    public class CountryService : ICountryService
    {
        private static readonly List<string> _countries = new()
        {
            "Colombia",
            "Perú",
            "México"
        };

        public Task<IEnumerable<string>> GetAllCountries()
        {
            return Task.FromResult<IEnumerable<string>>(_countries);
        }
    }
}