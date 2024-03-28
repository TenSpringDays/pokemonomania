using System;
using Infrastructure;
using Pokemonomania.Hud;
using Pokemonomania.Services;
using UnityEngine;


namespace Pokemonomania
{
    public class GameBootstrap : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private ScoreConfig _scoreConfig;
        [SerializeField] private KeyboardConfig _keyboardConfig;
        [SerializeField] private MainSceneHud _hudView;
        [SerializeField] private HudInput _hudInput;
        [SerializeField] private PokemonFactory _pokemonFactory;
        [SerializeField] private LooseCurtainView _looseCurtainView;

        private ProjectBootstrap _projectBootstrap;
        private ScoreService _scoreService;
        private ComboService _comboService;
        private TimerService _timerService;
        private InputService _inputService;
        private GameStateFlow _gameStateFlow;

        private void Start()
        {
            _projectBootstrap = GameObject.FindGameObjectWithTag("ProjectBootstrap").GetComponent<ProjectBootstrap>();
            
            _timerService = new TimerService();
            _comboService = new ComboService(_scoreConfig);
            _scoreService = new ScoreService(_scoreConfig, _comboService);
            _inputService = new InputService(new KeyboardInput(_keyboardConfig), _hudInput);
            _hudView.Construct(_timerService, _comboService, _scoreService);

            _gameStateFlow = new GameStateFlow(
                _pokemonFactory,
                _scoreService,
                _comboService,
                _inputService,
                this,
                _looseCurtainView,
                _timerService,
                _projectBootstrap.DataService
            );

            _hudView.Enable();
            _gameStateFlow.Enable();
        }

        private void Update()
        {
            float delta = Time.deltaTime;

            _inputService.Tick(delta);
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