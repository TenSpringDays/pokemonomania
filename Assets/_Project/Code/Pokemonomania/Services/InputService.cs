using System;
using Pokemonomania.Hud;
using UnityEngine;


namespace Pokemonomania.Services
{
    public class InputService : IInputService
    {
        private readonly KeyboardInput _keyboardInput;
        private readonly HudInput _hudInput;

        public InputService(KeyboardInput keyboardInput, HudInput hudInput)
        {
            _keyboardInput = keyboardInput;
            _hudInput = hudInput;
        }
        

        public event Action<int> Pressed;

        public void Enable(int maxInputIndexes)
        {
            _keyboardInput.Enable(maxInputIndexes);
            _hudInput.Enable(maxInputIndexes);

            _keyboardInput.Pressed += OnPressed;
            _hudInput.Pressed += OnPressed;
        }

        private void OnPressed(int index)
        {
            Pressed?.Invoke(index);
        }

        public void Disable()
        {
            _keyboardInput.Pressed -= OnPressed;
            _hudInput.Pressed -= OnPressed;
            
            _keyboardInput.Disable();
            _hudInput.Disable();
        }

        public void Tick(float delta)
        {
            _keyboardInput.Tick(delta);
        }

        public void LostFocus()
        {
            _keyboardInput.LostFocus();
        }
    }
}