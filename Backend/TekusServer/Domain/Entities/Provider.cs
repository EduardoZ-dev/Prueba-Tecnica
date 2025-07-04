using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public sealed class Provider : BaseEntity
    {
        private readonly List<CustomField> _customFields = new();
        private readonly List<Service> _services = new();
        public IReadOnlyCollection<CustomField> CustomFields => _customFields;
        public IReadOnlyCollection<Service> Services => _services;

        private Provider()
        {

        }
        public Provider(string nit, string name, Email email)
        {
            Nit = nit;
            Name = name;
            Email = email;
        }

        public string Nit { get; private set; }
        public string Name { get; private set; }
        public Email Email { get; private set; }

        public void AddCustomField(string key, string value)
        {
            var customField = CustomField.Create(key, value, this.Id);
            _customFields.Add(customField);
        }

        public Service AddService(string name, decimal rate, List<string> countries)
        {
            var service = Service.Create(name, rate, countries, this.Id);
            _services.Add(service);
            return service;
        }

        public void Update(string name, Email email)
        {
            Name = name;
            Email = email;
            SetUpdatedNow();
        }

        public void ClearCustomFields()
        {
            _customFields.Clear();
            SetUpdatedNow();
        }

        public void ClearServices()
        {
            _services.Clear();
            SetUpdatedNow();
        }

        public bool TryUpdateCustomField(string key, string newValue)
        {
            var customField = _customFields.FirstOrDefault(cf => cf.Key == key);
            if (customField is null) return false;

            customField.Update(newValue);
            return true;
        }

        public bool RemoveCustomField(Guid customFieldId)
        {
            var field = _customFields.FirstOrDefault(cf => cf.Id == customFieldId);
            if (field is null)
                return false;

            _customFields.Remove(field);
            SetUpdatedNow();
            return true;
        }
    }
}
