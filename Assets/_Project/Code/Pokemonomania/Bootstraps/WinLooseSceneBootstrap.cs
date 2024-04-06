using UnityEngine;
using VContainer;
using VContainer.Unity;


namespace Pokemonomania.Bootstrap
{
    public class WinLooseSceneBootstrap : LifetimeScope
    {
        [SerializeField] private UserStatsView _canvasPrefab;
        private bool _shouldContinue;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInNewPrefab(_canvasPrefab, Lifetime.Scoped);
            builder.RegisterBuildCallback(resolver =>
            {
                resolver.Resolve<UserStatsView>().Enable();
            });
        }
    }
}
