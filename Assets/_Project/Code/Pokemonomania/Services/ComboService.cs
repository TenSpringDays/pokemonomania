using UnityEngine.Events;


namespace Pokemonomania.Services
{
    public class ComboService
    {
        private readonly ScoreConfig _config;
        private int _currentStgae;
        private int _totalCombo;
        private int _maximumComboInOnePlay;
        private float _comboRangeTime;

        public ComboService(ScoreConfig config)
        {
            _config = config;
        }

        public int TotalCombo => _totalCombo;
        public ComboStage Stage => _config.MultiplyStages[_currentStgae];

        public event UnityAction<int> Changed;
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
                    UpdateMaximumComboInOnePlay(_totalCombo);
                }
            }
        }

        public void AddCombo()
        {
            _totalCombo += 1;
            _comboRangeTime = _config.DropComboDelay;
            Changed?.Invoke(_totalCombo);

            if (TryMoveNextStage())
                StageChanged?.Invoke(_config.MultiplyStages[_currentStgae]);
        }

        public void DropCombo()
        {
            _comboRangeTime = 0;
            UpdateMaximumComboInOnePlay(_totalCombo);
            ResetCombo();
            
            Changed?.Invoke(_totalCombo);
            StageChanged?.Invoke(_config.MultiplyStages[_currentStgae]);
        }

        private bool TryMoveNextStage()
        {
            if (_currentStgae < _config.MultiplyStages.Length - 1)
            {
                if (_totalCombo >= _config.MultiplyStages[_currentStgae + 1].RequiredCombo)
                {
                    _currentStgae++;
                    return true;
                }
            }

            return false;
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