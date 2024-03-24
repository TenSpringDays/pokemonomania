using UnityEngine;
using UnityEngine.Events;


namespace StoneBreaker
{
    public class InputObserver : MonoBehaviour
    {
        [System.Serializable]
        public struct EventPair
        {
            public string Key;
            public UnityEvent OnKeyDown;
            [System.NonSerialized] public float PressTime;
            [System.NonSerialized] public bool Pressed;
        }
        
        [SerializeField] private UnityEvent _onAnyKeyDown;
        [SerializeField] private EventPair[] _eventPairs;
        [SerializeField] private float _repeatDelay = 0.1f;

        public UnityEvent OnAnyKeyDown => _onAnyKeyDown;

        private void OnDisable()
        {
            for (int i = 0; i < _eventPairs.Length; i++)
            {
                ref var item = ref _eventPairs[i];

                if (item.Pressed)
                {
                    item.Pressed = false;
                }
            }
        }

        private void Update()
        {
            if (Input.anyKeyDown)
                _onAnyKeyDown?.Invoke();

            float time = Time.realtimeSinceStartup;
            
            UpdatePressRepeat(time);
            UpdateButtonDown(time);
            UpdateButtonUp();
        }

        private void UpdatePressRepeat(float time)
        {
            float delayed = time - _repeatDelay;
            
            for (int i = 0; i < _eventPairs.Length; i++)
            {
                ref var item = ref _eventPairs[i];

                if (item.Pressed && item.PressTime < delayed)
                {
                    item.PressTime += _repeatDelay;
                    item.OnKeyDown?.Invoke();
                    Debug.Log("repeat: " + item.Key);
                } 
            }
        }

        private void UpdateButtonDown(float time)
        {
            for (int i = 0; i < _eventPairs.Length; i++)
            {
                ref var item = ref _eventPairs[i];

                if (Input.GetButtonDown(item.Key))
                {
                    item.Pressed = true;
                    item.PressTime = time;
                    item.OnKeyDown?.Invoke();
                    Debug.Log("press: " + item.Key);
                }
            }
        }

        private void UpdateButtonUp()
        {
            for (int i = 0; i < _eventPairs.Length; i++)
            {
                ref var item = ref _eventPairs[i];

                if (item.Pressed && Input.GetButtonUp(item.Key))
                {
                    item.Pressed = false;
                    item.PressTime = 0f;
                    Debug.Log("release: " + item.Key);
                }
            }
        }
    }
}