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
            ScoreService.Instance.ScoreChanged += ScoreChanged;

            ScoreChanged(ScoreService.Instance.Score);
        }

        private void OnDisable()
        {
            if (ScoreService.Instance)
                ScoreService.Instance.ScoreChanged -= ScoreChanged;
        }

        private void ScoreChanged(int score)
        {
            _scoreText.SetText("{}", score);
        }
    }
}