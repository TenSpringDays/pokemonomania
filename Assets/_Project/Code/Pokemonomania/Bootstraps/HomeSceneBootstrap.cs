using Pokemonomania.Flows;
using UnityEngine;
using VContainer;
using VContainer.Unity;


namespace Pokemonomania.Bootstrap
{
    public class HomeSceneBootstrap : LifetimeScope
    {
        [SerializeField] private UserStatsView _userStatsViewPrefab;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInNewPrefab(_userStatsViewPrefab, Lifetime.Scoped);
            builder.RegisterEntryPoint<HomeSceneFlow>();
        }
    }
}