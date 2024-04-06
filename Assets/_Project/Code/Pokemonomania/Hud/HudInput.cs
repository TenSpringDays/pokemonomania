using System;
using UnityEngine;
using Utils;
using VContainer;


namespace Pokemonomania.Hud
{
    public class HudInput : MonoBehaviour
    {
        [SerializeField] private Button2[] _buttons;
        private RepeatingClick[] _clicks;
        private InputConfig _config;
        private bool _enabled;

        public event Action<int> Pressed;

        [Inject]
        public void Construct(InputConfig config)
        {
            _config = config;
        }

        public void Enable(int maxInputIndexes)
        {
            int min = Mathf.Min(maxInputIndexes, _buttons.Length);
            _clicks = new RepeatingClick[min];
            
            for (int i = 0; i < min; i++)
            {
                var index = i;
                _clicks[i] = new RepeatingClick(_config.RepeatDelay, () => Pressed?.Invoke(index));
                
                _buttons[i].Down.AddListener(() => _clicks[index].Down(Time.time, addRequest: true));
                _buttons[i].Up.AddListener(() => _clicks[index].Up());
                _buttons[i].gameObject.SetActive(true);
            }

            for (int i = min; i < _buttons.Length; i++)
                _buttons[i].gameObject.SetActive(false);

            _enabled = true;
        }

        public void Disable()
        {
            Pressed = null;
            for (int i = 0; i < _buttons.Length; i++)
            {
                _buttons[i].Down.RemoveAllListeners();
                _buttons[i].Up.RemoveAllListeners();
            }

            _enabled = false;
        }

        private void Update()
        {
            UpdateState(Time.time);
        }

        private void UpdateState(float time)
        {
            if (!_enabled)
                return;
            
            for (int i = 0; i < _clicks.Length; i++)
            {
                _clicks[i].Delay = _config.RepeatDelay;
                _clicks[i].Tick(time);
            }

            for (int i = 0; i < _clicks.Length; i++)
                _clicks[i].InvokeRequests(oneTime: true);
        }


    }
}