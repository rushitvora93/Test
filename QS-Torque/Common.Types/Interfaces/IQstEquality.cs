namespace Core
{
    public interface IEqualsByContent<T>
    {
        bool EqualsByContent(T other);
    }


    public interface IQstEquality<T> : IEqualsByContent<T>
    {
        bool EqualsById(T other);
    }
}
