using UnityEngine;
using UnityEngine.Events;


namespace Pokemonomania.Services
{
    public class ComboService
    {
        private readonly ScoreConfig _config;
        private int _currentStgae;
        private int _comboCount;
        private int _maximumComboInOnePlay;
        private float _comboRangeTime;

        public ComboService(ScoreConfig config)
        {
            _config = config;
        }

        public int ComboCount => _comboCount;
        public ComboStage Stage => _config.MultiplyStages[_currentStgae];
        public int MaxCombo => _maximumComboInOnePlay;

        public event UnityAction<int> CountChanged;
        public event UnityAction<ComboStage> StageChanged;

        public void Tick(float delta)
        {
            if (_comboRangeTime > 0)
            {
                _comboRangeTime -= delta;

                if (_comboRangeTime <= 0f)
                {
                    DropCombo();
                    ResetCombo();
                }
            }
        }

        public void AddCombo()
        {
            _comboCount += 1;
            _maximumComboInOnePlay = Mathf.Max(_maximumComboInOnePlay, _comboCount);
            _comboRangeTime = _config.DropComboDelay;
            CountChanged?.Invoke(_comboCount);

            if (TryMoveNextStage())
                StageChanged?.Invoke(_config.MultiplyStages[_currentStgae]);
        }

        public void DropCombo()
        {
            _comboRangeTime = 0;
            ResetCombo();
            
            CountChanged?.Invoke(_comboCount);
            StageChanged?.Invoke(_config.MultiplyStages[_currentStgae]);
        }

        private bool TryMoveNextStage()
        {
            if (_currentStgae < _config.MultiplyStages.Length - 1)
            {
                if (_comboCount >= _config.MultiplyStages[_currentStgae + 1].RequiredCombo)
                {
                    _currentStgae++;
                    return true;
                }
            }

            return false;
        }

        private void ResetCombo()
        {
            _comboCount = 0;
            _currentStgae = 0;
        }
    }
}