using System;
using UnityEngine;


namespace StoneBreaker.Infrastructure
{
    public abstract class Singleton<T> : MonoBehaviour
        where T: MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (!_instance)
                    _instance = FindObjectOfType<T>(true);

                return _instance;
            }
        }
    }
}