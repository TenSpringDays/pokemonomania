using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Pokemonomania.Hud
{
    [AddComponentMenu("UI/Button 2", 30)]
    public class Button2 : Selectable
    {
        
        [SerializeField] private UnityEvent _up;
        [SerializeField] private UnityEvent _down;

        public UnityEvent Up => _up;
        public UnityEvent Down => _down;

        public override void OnPointerDown(PointerEventData eventData)
        {
            if (IsActive() && IsInteractable())
            {
                base.OnPointerDown(eventData);
                _down?.Invoke();
            }
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            if (IsActive() && IsInteractable())
            {
                base.OnPointerUp(eventData);
                _up?.Invoke();
            }
        }
    }
}