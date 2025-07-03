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

        public static CustomField Create(string key, string value)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Key is required", nameof(key));

            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Value is required", nameof(value));

            return new CustomField(key, value);
        }

        public string Key { get; private set; } = null!;
        public string Value { get; private set; } = null!;

        public void Update(string value)
        {
            Value = value;
            SetUpdatedNow();
        }
    }
}
