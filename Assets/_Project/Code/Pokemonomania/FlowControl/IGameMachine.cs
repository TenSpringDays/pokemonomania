namespace Pokemonomania.FlowControl
{
    public interface IGameMachine
    {
        GameState State { get; }

        void Play();

        void Pause();

        void Resume();

        void Finish();

        void AddListener(object listener);

        void RemoveListener(object listener);
    }
}