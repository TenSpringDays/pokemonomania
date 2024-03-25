using Infrastructure;
using UnityEngine;
using UnityEngine.Events;


namespace StoneBreaker
{
    public class ScoreManager : Singleton<ScoreManager>
    {
        [SerializeField] private int _scorePerBreaking;

        private int _score;

        public int Score => _score;
        public event UnityAction<int> ScoreChanged;

        public void AddScore()
        {
            float mul = ComboManager.Instance.Stage.Multiplying;
            _score += Mathf.RoundToInt(_scorePerBreaking * mul);
            ScoreChanged?.Invoke(_score);
        }
    }
}