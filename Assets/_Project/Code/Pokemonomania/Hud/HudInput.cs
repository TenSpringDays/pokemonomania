using System;
using UnityEngine;
using UnityEngine.UI;


namespace Pokemonomania.Hud
{
    public class HudInput : MonoBehaviour, IInputService
    {
        [SerializeField] private Button[] _buttons;

        public event Action<int> Pressed;

        public void Enable(int maxInputIndexes)
        {
            int min = Mathf.Min(maxInputIndexes, _buttons.Length);
            
            for (int i = 0; i < min; i++)
            {
                var index = i;
                _buttons[i].onClick.AddListener(() =>
                {
                    Pressed?.Invoke(index);
                });
                _buttons[i].gameObject.SetActive(true);
            }

            for (int i = min; i < _buttons.Length; i++)
                _buttons[i].gameObject.SetActive(false);
        }

        public void Disable()
        {
            for (int i = 0; i < _buttons.Length; i++)
            {
                _buttons[i].onClick.RemoveAllListeners();
            }
        }
        
        
    }
}