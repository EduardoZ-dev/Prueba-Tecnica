using Domain.Exceptions;

namespace Domain.ValueObjects
{
    public class Email : IEquatable<Email>
    {
        public string Value { get; }

        private Email(string value)
        {
            Value = value;
        }

        public static Email Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || !value.Contains("@"))
                throw new DomainException("Invalid email format");

            return new Email(value);
        }

        public override string ToString() => Value;

        public override bool Equals(object? obj) => Equals(obj as Email);

        public bool Equals(Email? other) => other is not null && Value == other.Value;

        public override int GetHashCode() => Value.GetHashCode();
    }
}