namespace Infrastructure
{
    public interface IServiceLocator
    {
        void Add(object service);

        void Remove(object service);

        T Get<T>();

        T[] GetAll<T>();
    }
}