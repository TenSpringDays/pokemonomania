using System;
using Pokemonomania.Hud;
using UnityEngine;
using Utils;


namespace Pokemonomania
{
    public class KeyboardInput : IInputService
    {
        private readonly InputConfig _config;
        private RepeatingClick[] _clicks;
        private bool _enabled;

        public KeyboardInput(InputConfig config)
        {
            _config = config;
        }

        public event Action<int> Pressed;

        public void Enable(int maxInputIndexes)
        {
            var count = Mathf.Min(_config.Keys.Length, maxInputIndexes);
            _clicks = new RepeatingClick[count];

            for (int i = 0; i < count; i++)
            {
                var index = i;
                _clicks[i] = new RepeatingClick(
                    _config.RepeatDelay,
                    () => Pressed?.Invoke(index));
            }
            
            _enabled = true;
        }

        public void Disable()
        {
            Pressed = null;
            _enabled = false;
        }

        public void Tick(float time)
        {
            if (!_enabled)
                return;

            for (int i = 0; i < _clicks.Length; i++)
            {
                var variants = _config.Keys[i].Variants;

                for (int j = 0; j < variants.Length; j++)
                {
                    if (Input.GetKeyDown(variants[j]))
                    {
                        _clicks[i].Down(time, addRequest: true);
                        break;
                    }

                    if (Input.GetKeyUp(variants[j]))
                        _clicks[i].Up();
                }
            }

            for (int i = 0; i < _clicks.Length; i++)
            {
                _clicks[i].Delay = _config.RepeatDelay;
                _clicks[i].Tick(time);
            }

            for (int i = 0; i < _clicks.Length; i++)
                _clicks[i].InvokeRequests(oneTime: true);
        }

        public void LostFocus()
        {
            for (int i = 0; i < _clicks.Length; i++)
                _clicks[i].Up();
        }
    }
}