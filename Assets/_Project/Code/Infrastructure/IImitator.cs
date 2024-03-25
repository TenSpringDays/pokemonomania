namespace Infrastructure
{
    public interface IImitator<TFor>
    {
        void Imitate(TFor other);
    }
}