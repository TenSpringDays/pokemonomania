using System;
using System.Runtime.CompilerServices;
using UnityEngine;


namespace Infrastructure
{
    public abstract class Singleton<T> : MonoBehaviour
        where T: MonoBehaviour
    {
        private static T _instance;
        private static Action<T> _onInstantiates;

        public static T Instance => _instance;

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetInstance(out T instance)
        {
            instance = _instance;
            return instance;
        }

        public static void WaitInstance(Action<T> onInstanciate)
        {
            _onInstantiates += onInstanciate;
        }

        private void Awake()
        {
            if (_instance)
                throw new UnityException($"[SINGLETON] Try create >1 instance for {typeof(T)} singleton");

            _instance = this as T;
            _onInstantiates?.Invoke(_instance);
            _onInstantiates = null;
        }

        private void OnDestroy()
        {
            _onInstantiates = null;
            _instance = null;
        }
    }
}