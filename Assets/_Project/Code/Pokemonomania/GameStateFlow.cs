using System.Collections;
using Infrastructure;
using Pokemonomania.Data;
using Pokemonomania.Hud;
using Pokemonomania.Services;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Pokemonomania
{
    public class GameStateFlow
    {
        private readonly PokemonFactory _pokemonFactory;
        private readonly ScoreService _scoreService;
        private readonly ComboService _comboService;
        private readonly IInputService _inputService;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly LooseCurtainView _looseCurtainView;
        private readonly TimerService _timerService;
        private readonly IDataService _dataService;

        public GameStateFlow(PokemonFactory pokemonFactory,
                             ScoreService scoreService,
                             ComboService comboService,
                             IInputService inputService,
                             ICoroutineRunner coroutineRunner,
                             LooseCurtainView looseCurtainView,
                             TimerService timerService,
                             IDataService dataService)
        {
            _pokemonFactory = pokemonFactory;
            _scoreService = scoreService;
            _comboService = comboService;
            _inputService = inputService;
            _coroutineRunner = coroutineRunner;
            _looseCurtainView = looseCurtainView;
            _timerService = timerService;
            _dataService = dataService;
        }

        public void Enable()
        {
            _timerService.Enabled = true;
            _inputService.Enable(maxInputIndexes:2);
            _inputService.Pressed += OnPressed;
        }

        public void Disable()
        {
            _inputService.Pressed -= OnPressed;
            _inputService.Disable();
        }

        public void Tick(float delta)
        {
        }

        private void OnPressed(int index)
        {
            if (_pokemonFactory.CurrentId == index)
                SuccessfullyCatch();
            else
                _coroutineRunner.StartCoroutine(UnsuccessfullyCatch());
        }

        private void SuccessfullyCatch()
        {
            _pokemonFactory.Pop();
            _scoreService.AddScore();
            _comboService.AddCombo();
        }

        private IEnumerator UnsuccessfullyCatch()
        {
            Disable();
            _timerService.Enabled = false;
            _comboService.DropCombo();
            _looseCurtainView.Show();
            UpdateUserStats();

            yield return new WaitForSecondsRealtime(1f);
            yield return SceneManager.LoadSceneAsync("winloseScene", LoadSceneMode.Single);
        }

        private void UpdateUserStats()
        {
            var stats = _dataService.LoadUserStats();

            float elapsed = _timerService.Elapsed;
            int score = _scoreService.Score;
            int combo = _comboService.MaxCombo;

            stats.Last.Elapsed = elapsed;
            stats.Last.Score = score;
            stats.Last.Combo = combo;

            stats.Best.Elapsed = Mathf.Max(stats.Best.Elapsed, elapsed);
            stats.Best.Score = Mathf.Max(stats.Best.Score, score);
            stats.Best.Combo = Mathf.Max(stats.Best.Combo, combo);

            stats.LastEndGameStatus = EndGameStatus.Loose;
            
            _dataService.SaveUserStats(stats);
        }
    }
}