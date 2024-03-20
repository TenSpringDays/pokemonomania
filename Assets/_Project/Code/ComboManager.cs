using StoneBreaker.Infrastructure;
using UnityEngine;
using UnityEngine.Events;


namespace StoneBreaker
{
    public class ComboManager : Singleton<ComboManager>
    {
        [SerializeField] private float maxComboRangeTime;
        [SerializeField] private ScoreMultiplyStage[] _multiplyStages;

        private int _currentStgae;
        private int _totalCombo;
        private int _maximumComboInOnePlay;
        private float _comboRangeTime;

        public int TotalCombo => _totalCombo;
        public ScoreMultiplyStage Stage => _multiplyStages[_currentStgae];

        public event UnityAction<int> ComboChanged;
        public event UnityAction<ScoreMultiplyStage> ScoreMultiplyChanged;

        private void OnEnable()
        {
            ResetCombo();
            _maximumComboInOnePlay = 0;
            _comboRangeTime = 0;
        }

        private void Update()
        {
            if (_comboRangeTime > 0)
            {
                _comboRangeTime -= Time.deltaTime;

                if (_comboRangeTime <= 0f)
                {
                    DropCombo();
                    ResetCombo();
                    UpdateMaximumComboInOnePlay(_totalCombo);
                }
            }
        }

        public void AddCombo()
        {
            _totalCombo += 1;
            _comboRangeTime = maxComboRangeTime;
            ComboChanged?.Invoke(_totalCombo);

            if (TryMoveNextStage())
                ScoreMultiplyChanged?.Invoke(_multiplyStages[_currentStgae]);
        }

        private bool TryMoveNextStage()
        {
            if (_currentStgae < _multiplyStages.Length - 1)
            {
                if (_totalCombo >= _multiplyStages[_currentStgae + 1].TotalCombo)
                {
                    _currentStgae++;
                    return true;
                }
            }

            return false;
        }

        public void DropCombo()
        {
            _comboRangeTime = 0;
            UpdateMaximumComboInOnePlay(_totalCombo);
            ResetCombo();
            
            ComboChanged?.Invoke(_totalCombo);
            ScoreMultiplyChanged?.Invoke(_multiplyStages[_currentStgae]);
        }

        private void ResetCombo()
        {
            _totalCombo = 0;
            _currentStgae = 0;
        }

        private void UpdateMaximumComboInOnePlay(int newTotalCombo)
        {
            if (newTotalCombo > _maximumComboInOnePlay)
            {
                _maximumComboInOnePlay = newTotalCombo;
            }
        }
    }
}