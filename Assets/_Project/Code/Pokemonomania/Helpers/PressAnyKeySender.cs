using UnityEngine;
using UnityEngine.Events;


namespace Pokemonomania
{
    public class PressAnyKeySender : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onAnyKeyDown;

        private void Update()
        {
            if (Input.anyKeyDown)
                _onAnyKeyDown?.Invoke();
        }
    }
}
