using Pokemonomania.Services;
using Pokemonomania.StaticData;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Pokemonomania
{
    public class ProjectBootstrap : MonoBehaviour
    {
        [SerializeField] private GameResourcesData _gameResourcesData;

        public GameResourcesData GameResourcesData => _gameResourcesData;
        public IDataService DataService { get; private set; }

        private void Start()
        {
            DataService = new DesktopDataService();
            
            DontDestroyOnLoad(this);
            SceneManager.LoadScene("1. Home", LoadSceneMode.Single);
        }

        public static ProjectBootstrap Find()
        {
            return GameObject.FindGameObjectWithTag("ProjectBootstrap").GetComponent<ProjectBootstrap>();
        }
    }
}