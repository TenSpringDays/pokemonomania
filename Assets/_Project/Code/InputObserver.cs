using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;


namespace StoneBreaker
{
    public class InputObserver : MonoBehaviour
    {
        [System.Serializable]
        public struct EventPair
        {
            public string Key;
            public UnityEvent OnKeyDown;
        }
        
        [FormerlySerializedAs("onAnyKeyDown")] [SerializeField] private UnityEvent _onAnyKeyDown;
        [SerializeField] private EventPair[] _eventPairs;

        public UnityEvent OnAnyKeyDown => _onAnyKeyDown;

        private void Update()
        {
            if (Input.anyKeyDown)
                _onAnyKeyDown?.Invoke();

            for (int i = 0; i < _eventPairs.Length; i++)
            {
                if (Input.GetButtonDown(_eventPairs[i].Key))
                    _eventPairs[i].OnKeyDown?.Invoke();
            }
        }
    }
}