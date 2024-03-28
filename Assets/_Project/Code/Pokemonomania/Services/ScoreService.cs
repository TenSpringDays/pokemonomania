using UnityEngine;
using UnityEngine.Events;


namespace Pokemonomania.Services
{
    public class ScoreService
    {
        private readonly ScoreConfig _config;
        private readonly ComboService _comboService;
        private int _score;

        public ScoreService(ScoreConfig config, ComboService comboService)
        {
            _config = config;
            _comboService = comboService;
        }

        public int Score => _score;
        
        public event UnityAction<int> Changed;

        public void AddScore()
        {
            float mul = _comboService.Stage.Multiplier;
            _score += Mathf.RoundToInt(_config.ScorePerCatch * mul);
            Changed?.Invoke(_score);
        }
    }
}