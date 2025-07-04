using Domain.Common;

namespace Domain.Entities
{
    public sealed class CustomField : BaseEntity
    {
        private CustomField()
        {

        }

        private CustomField(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public static CustomField Create(string key, string value, Guid providerId)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Key is required", nameof(key));

            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Value is required", nameof(value));

            if (providerId == Guid.Empty)
                throw new ArgumentException("ProviderId is required", nameof(providerId));

            var field = new CustomField(key, value);
            field.ProviderId = providerId;
            field.Id = Guid.NewGuid();
            return field;
        }

        public string Key { get; private set; } = null!;
        public string Value { get; private set; } = null!;
        public Guid ProviderId { get; private set; }

        public void Update(string value)
        {
            Value = value;
            SetUpdatedNow();
        }
    }
}
