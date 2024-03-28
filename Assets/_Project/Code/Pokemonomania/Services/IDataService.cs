using Pokemonomania.Data;


namespace Pokemonomania.Services
{
    public interface IDataService
    {
        UserStats LoadUserStats();

        void SaveUserStats(UserStats stats);
    }
}