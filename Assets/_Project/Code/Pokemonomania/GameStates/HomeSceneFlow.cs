using VContainer.Unity;


namespace Pokemonomania.Flows
{
    public class HomeSceneFlow : IStartable
    {
        private readonly UserStatsView _userStatsView;

        public HomeSceneFlow(UserStatsView userStatsView)
        {
            _userStatsView = userStatsView;
        }

        public void Start()
        {
            _userStatsView.Enable();
        }
    }
}