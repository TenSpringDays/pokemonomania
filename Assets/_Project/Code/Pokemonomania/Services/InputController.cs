using System;
using Pokemonomania.Hud;


namespace Pokemonomania.Services
{
    public class InputController
    {
        private readonly KeyboardInput _keyboardInput;
        private readonly HudInput _hudInput;

        public InputController(KeyboardInput keyboardInput, HudInput hudInput)
        {
            _keyboardInput = keyboardInput;
            _hudInput = hudInput;
        }
        

        public event Action<int> Pressed;

        private void OnPressed(int id)
        {
            Pressed?.Invoke(id);
        }

        public void Enable(int maxInputIndexes)
        {
            _keyboardInput.Enable(maxInputIndexes);
            _hudInput.Enable(maxInputIndexes);
            
            _keyboardInput.Pressed += OnPressed;
            _hudInput.Pressed += OnPressed;
        }

        public void Disable()
        {
            _keyboardInput.Disable();
            _hudInput.Disable();
            
            Pressed -= OnPressed;
        }
    }
}