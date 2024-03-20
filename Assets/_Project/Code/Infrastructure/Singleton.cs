using System;
using UnityEngine;


namespace StoneBreaker.Infrastructure
{
    public abstract class Singleton<T> : MonoBehaviour
        where T: MonoBehaviour
    {
        private static T _instance;
        private static Action<T> _onInstantiates;

        public static T Instance
        {
            get
            {
                if (!_instance)
                    _instance = FindObjectOfType<T>(true);

                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance)
                throw new UnityException($"[SINGLETON] Try create >1 instance for {typeof(T)} singleton");

            _instance = this as T;
            _onInstantiates?.Invoke(_instance);
            _onInstantiates = null;
        }

        public static void LazyInstance(Action<T> onInstanciate)
        {
            _onInstantiates += onInstanciate;
        }
    }
}