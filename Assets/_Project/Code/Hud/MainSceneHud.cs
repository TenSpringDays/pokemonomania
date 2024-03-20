using System;
using TMPro;
using UnityEngine;


namespace StoneBreaker.Hud
{
    public class MainSceneHud : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _comboText;
        [SerializeField] private TMP_Text _multiplierText;
        [SerializeField] private TMP_Text _timeText;

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