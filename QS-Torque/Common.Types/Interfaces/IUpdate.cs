namespace Core
{
    public interface IUpdate<T>
    {
        void UpdateWith(T other);
    }
}
