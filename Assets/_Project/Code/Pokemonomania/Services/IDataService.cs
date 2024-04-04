using Pokemonomania.Data;


namespace Pokemonomania.Services
{
    public interface IDataService
    {
        T Load<T>() where T : new();

        void Save<T>(T data) where T : new();
    }
}