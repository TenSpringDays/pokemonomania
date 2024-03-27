using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Pokemonomania.Hud
{
    public class MainSceneHud : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _comboText;
        [SerializeField] private TMP_Text _multiplierText;
        [SerializeField] private TMP_Text _timeText;
        [SerializeField] private Image _timeProgres;

        private void OnEnable()
        {
            ScoreManager.Instance.ScoreChanged += OnScoreChanged;
            ComboManager.Instance.ComboChanged += OnComboChanged;
            ComboManager.Instance.ScoreMultiplyChanged += OnScoreMultiplyChanged;

            OnScoreChanged(ScoreManager.Instance.Score);
            OnComboChanged(ComboManager.Instance.TotalCombo);
            OnScoreMultiplyChanged(ComboManager.Instance.Stage);
        }

        private void OnDisable()
        {
            if (ScoreManager.Instance)
                ScoreManager.Instance.ScoreChanged -= OnScoreChanged;
        }

        private void LateUpdate()
        {
            UpdateTimer();
        }

        private void UpdateTimer()
        {
            var tm = TimerManager.Instance;
            _timeText.SetText("{0.00}", tm.Time);
            _timeProgres.fillAmount = tm.Time / tm.MaxTime;
        }

        private void OnScoreMultiplyChanged(ScoreMultiplyStage arg0)
        {
            _multiplierText.SetText("{0.00}", arg0.Multiplying);
            _comboText.color = arg0.ComboTextColor;
        }

        private void OnComboChanged(int arg0)
        {
            _comboText.SetText("{}", arg0);
        }

        private void OnScoreChanged(int score)
        {
            _scoreText.SetText("{}", score);
        }
    }
}