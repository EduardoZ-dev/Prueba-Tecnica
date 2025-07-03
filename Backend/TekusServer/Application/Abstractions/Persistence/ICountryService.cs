namespace Application.Abstractions.Persistence
{
    public interface ICountryService
    {
        Task<IEnumerable<string>> GetAllCountries();
    }
}
