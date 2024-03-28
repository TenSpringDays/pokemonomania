using System;
using Pokemonomania.Hud;
using Pokemonomania.Services;
using UnityEngine;


namespace Pokemonomania
{
    public class GameBootstrap : MonoBehaviour
    {
        [SerializeField] private ScoreConfig _scoreConfig;
        [SerializeField] private KeyboardConfig _keyboardConfig;
        [SerializeField] private MainSceneHud _hudView;
        [SerializeField] private HudInput _hudInput;
        [SerializeField] private PokemonFactory _pokemonFactory;

        private ScoreService _scoreService;
        private ComboService _comboService;
        private TimerService _timerService;
        private KeyboardInputService _inputService;

        private void Start()
        {
            _timerService = new TimerService();
            _comboService = new ComboService(_scoreConfig);
            _scoreService = new ScoreService(_scoreConfig, _comboService);
            _inputService = new KeyboardInputService(_keyboardConfig);
            
            _hudView.Construct(_timerService, _comboService, _scoreService);

            _hudView.Enable();
            _hudInput.Enable(2);
            _inputService.Enable(2);
        }

        private void Update()
        {
            float delta = Time.deltaTime;
            
            _inputService.Tick(delta);
            _timerService.Tick(delta);
            _comboService.Tick(delta);
            _hudView.Tick(delta);
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            _inputService?.LostFocus();
        }

        private void OnDestroy()
        {
        }
    }
}
