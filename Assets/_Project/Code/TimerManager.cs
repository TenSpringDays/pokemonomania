using StoneBreaker.Infrastructure;
using UnityEngine;
using UnityEngine.UI;


namespace StoneBreaker
{
    public class TimerManager : Singleton<TimerManager>
    {
        [SerializeField] private int maxTime;
        [SerializeField] private int decreasingSpeed = 1;

        private float _time;

        public float Time => _time;
        public float MaxTime => maxTime;

        private void OnEnable()
        {
            _time = maxTime;
        }

        private void Update()
        {
            _time -= decreasingSpeed * UnityEngine.Time.deltaTime;
            if (_time <= 0f)
            {
                _time = 0f;
                WinLoseManager.Instance.ChangeState(WinLoseManager.WinloseState.IsGameOver);
            }
        }
    }
}