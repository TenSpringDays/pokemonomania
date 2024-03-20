namespace StoneBreaker.Infrastructure
{
    public interface IEventReceiver<T>
    {
        void Receive(object sender, T argument);
    }
}