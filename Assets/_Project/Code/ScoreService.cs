using System;
using StoneBreaker.Infrastructure;
using UnityEngine;
using UnityEngine.Events;


namespace StoneBreaker
{
    public class ScoreService : Singleton<ScoreService>
    {
        [SerializeField] private ScoreMultiplyStage[] _multiplyStages;
        [SerializeField] private int _scorePerBreaking;

        private int _score;

        public int Score => _score;
        public event UnityAction<int> ScoreChanged;

        public void AddScore()
        {
            int i = FindMultiplyIndex();
            _score += Mathf.RoundToInt(_scorePerBreaking * _multiplyStages[i].Multiplying);
            ScoreChanged?.Invoke(_score);
        }
        
        private int FindMultiplyIndex()
        {
            int combo = ComboManager.instance.getCombo();
            
            for (int i = _multiplyStages.Length - 1; i >= 0; i--)
            {
                if (combo >= _multiplyStages[i].TotalCombo)
                    return i;
            }

            return -1;
        }
    }
}