using System;
using Infrastructure;
using Pokemonomania.Hud;
using Pokemonomania.Services;
using Pokemonomania.StaticData;
using UnityEngine;


namespace Pokemonomania
{
    public class GameBootstrap : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private ScoreConfig _scoreConfig;
        [SerializeField] private InputConfig _inputConfig;
        [SerializeField] private MainSceneHud _hudView;
        [SerializeField] private HudInput _hudInput;
        [SerializeField] private PokemonFactory _pokemonFactory;
        [SerializeField] private LooseCurtainView _looseCurtainView;
        [SerializeField] private CatchEffectController _catchEffectController;

        private IDataService _dataService;
        private GameResourcesData _gameResourcesData;
        private ScoreService _scoreService;
        private ComboService _comboService;
        private TimerService _timerService;
        private InputService _inputService;
        private GameStateFlow _gameStateFlow;

        private void Start()
        {
            var projectBootstrap = GameObject.FindGameObjectWithTag("ProjectBootstrap")
                .GetComponent<ProjectBootstrap>();

            _gameResourcesData = projectBootstrap.GameResourcesData;
            _dataService = projectBootstrap.DataService;
            
            _hudInput.Construct(_inputConfig);
            _timerService = new TimerService();
            _comboService = new ComboService(_scoreConfig);
            _scoreService = new ScoreService(_scoreConfig, _comboService);
            _inputService = new InputService(new KeyboardInput(_inputConfig), _hudInput);
            _hudView.Construct(_timerService, _comboService, _scoreService);
            _catchEffectController.Construct(_pokemonFactory, _gameResourcesData, _dataService);

            _gameStateFlow = new GameStateFlow(
                _pokemonFactory,
                _scoreService,
                _comboService,
                _inputService,
                this,
                _looseCurtainView,
                _timerService,
                _dataService
            );

            _hudView.Enable();
            _gameStateFlow.Enable();
        }

        private void Update()
        {
            float delta = Time.deltaTime;
            float time = Time.time;

            _inputService.Tick(time);
            _timerService.Tick(delta);
            _comboService.Tick(delta);
            _hudView.Tick(delta);

            _gameStateFlow.Tick(delta);
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            _inputService?.LostFocus();
        }

        private void OnDestroy()
        {
            _gameStateFlow.Disable();
        }
    }
}