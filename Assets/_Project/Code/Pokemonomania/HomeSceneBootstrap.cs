using UnityEngine;


namespace Pokemonomania
{
    public class HomeSceneBootstrap : MonoBehaviour
    {
        [SerializeField] private UserStatsView _userStatsView;

        private void Start()
        {
            var projectBootstra = ProjectBootstrap.Find();
            _userStatsView.Construct(projectBootstra.DataService);
            _userStatsView.Enable();
        }
    }
}