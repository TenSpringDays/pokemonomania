using Infrastructure;
using Pokemonomania.Data;
using Pokemonomania.Flows;
using Pokemonomania.Hud;
using Pokemonomania.Services;
using UnityEngine;
using VContainer;
using VContainer.Unity;


namespace Pokemonomania.Bootstrap
{
    public class GameBootstrap : LifetimeScope, ICoroutineRunner
    {
        [SerializeField] private ScoreConfig _scoreConfig;
        [SerializeField] private InputConfig _inputConfig;
        [SerializeField] private MainSceneHud _hudView;
        [SerializeField] private HudInput _hudInput;
        [SerializeField] private PokemonFactory _pokemonFactory;
        [SerializeField] private LooseCurtainView _looseCurtainView;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register(resolver => resolver.Resolve<GameSceneData>(), Lifetime.Scoped);
            builder.RegisterInstance(_scoreConfig);
            builder.RegisterInstance(_inputConfig);
            builder.RegisterInstance(_pokemonFactory);
            builder.RegisterInstance(_looseCurtainView);
            builder.Register<TimerService>(Lifetime.Scoped);
            builder.Register<ComboService>(Lifetime.Scoped);
            builder.Register<ScoreService>(Lifetime.Scoped);
            builder.Register<GameController>(Lifetime.Scoped);
            builder.Register<InputController>(Lifetime.Scoped);
            builder.RegisterInstance(_hudInput).AsSelf();
            builder.RegisterInstance(_hudView);
            
            builder.RegisterEntryPoint<KeyboardInput>(Lifetime.Scoped).AsSelf();
            builder.RegisterEntryPoint<GameSceneFlow>();
        }
    }
}