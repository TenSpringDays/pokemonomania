using Infrastructure;
using Pokemonomania.Services;
using Pokemonomania.StaticData;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using VContainer;
using VContainer.Unity;


namespace Pokemonomania.Bootstrap
{
    public class ProjectBootstrap : LifetimeScope
    {
        [FormerlySerializedAs("_gameResourcesData")] [SerializeField] private GameResourcesConfig gameResourcesConfig;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(gameResourcesConfig);
            builder.Register<DesktopDataService>(Lifetime.Singleton).As<IDataService>();
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