using Infrastructure;
using Pokemonomania.Services;
using Pokemonomania.StaticData;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;


namespace Pokemonomania.Bootstrap
{
    public class ProjectBootstrap : LifetimeScope
    {
        [SerializeField] private GameResourcesData _gameResourcesData;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_gameResourcesData);
            builder.Register<IDataService>(_ => new DesktopDataService(), Lifetime.Singleton);
            builder.Register(_ => gameObject.AddComponent<AppEventsProvider>(), Lifetime.Singleton);
            builder.Register<ICoroutineRunner>(_ => gameObject.AddComponent<CoroutineRunner>(), Lifetime.Singleton);
        }

        private void Start()
        {
            DontDestroyOnLoad(this);
            SceneManager.LoadScene("1. Home", LoadSceneMode.Single);
        }
    }
}