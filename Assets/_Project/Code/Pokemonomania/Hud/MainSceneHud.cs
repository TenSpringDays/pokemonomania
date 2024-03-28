using TMPro;
using UnityEngine;
using Pokemonomania.Services;


namespace Pokemonomania.Hud
{
    public class MainSceneHud : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _comboText;
        [SerializeField] private TMP_Text _multiplierText;
        [SerializeField] private TMP_Text _timeText;
        
        private TimerService _timeService;
        private ComboService _comboService;
        private ScoreService _scoreService;

        public void Construct(TimerService timerService, ComboService comboService, ScoreService scoreService)
        {
            _timeService = timerService;
            _comboService = comboService;
            _scoreService = scoreService;
        }
        
        public void Enable()
        {
            _scoreService.Changed += OnScoreChanged;
            _comboService.CountChanged += OnComboCountChanged;
            _comboService.StageChanged += OnComboStageChanged;
            
            OnScoreChanged(_scoreService.Score);
            OnComboCountChanged(_comboService.ComboCount);
            OnComboStageChanged(_comboService.Stage);
        }

        public void Disable()
        {
            _scoreService.Changed -= OnScoreChanged;
            _comboService.CountChanged -= OnComboCountChanged;
            _comboService.StageChanged -= OnComboStageChanged;
        }

        public void Tick(float delta)
        {
            UpdateTimer();
        }

        private void UpdateTimer()
        {
            var time = _timeService.Elapsed;
            _timeText.SetText("{0.00}", time);
        }

        private void OnComboStageChanged(ComboStage stage)
        {
            _multiplierText.SetText("{0.00}", stage.Multiplier);
            _comboText.color = stage.TextColor;
        }

        private void OnComboCountChanged(int combo)
        {
            _comboText.SetText("{}", combo);
        }

        private void OnScoreChanged(int score)
        {
            _scoreText.SetText("{}", score);
        }
    }
}