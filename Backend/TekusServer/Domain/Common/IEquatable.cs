namespace Domain.Common
{
    public interface IEquatable<T>
    {
        bool Equals(T? other);
    }
}
