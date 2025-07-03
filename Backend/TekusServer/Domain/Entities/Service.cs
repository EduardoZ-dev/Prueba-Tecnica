using Domain.Common;

namespace Domain.Entities
{
    public sealed class Service : BaseEntity
    {
        public string Name { get; private set; } = null!;
        public decimal HourlyRateUsd { get; private set; }

        private readonly List<string> _countries = new();
        public IReadOnlyCollection<string> Countries => _countries.AsReadOnly();

        private Service()
        {

        }

        private Service(string name, decimal hourlyRateUsd, List<string> countries)
        {
            Name = name;
            HourlyRateUsd = hourlyRateUsd;
            _countries = countries ?? new List<string>();
        }

        public static Service Create(string name, decimal hourlyRateUsd, List<string> countries)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required", nameof(name));

            if (hourlyRateUsd <= 0)
                throw new ArgumentException("Hourly rate must be positive", nameof(hourlyRateUsd));


            return new Service(name, hourlyRateUsd, countries);
        }

        public void Update(string name, decimal hourlyRateUsd)
        {
            Name = name;
            HourlyRateUsd = hourlyRateUsd;
            SetUpdatedNow();
        }

        public void AddCountry(string country)
        {
            if (!_countries.Contains(country))
                _countries.Add(country);
        }

        public void RemoveCountry(string country)
        {
            _countries.Remove(country);
        }
    }
}
