﻿using System;
using UnityEngine;


namespace Pokemonomania.Services
{
    public class AppEventsProvider : MonoBehaviour
    {
        public event Action<bool> ApplicationFocus;
        public event Action<bool> ApplicationPause;
        public event Action ApplicationQuit;

        private void OnApplicationFocus(bool hasFocus)
        {
            ApplicationFocus?.Invoke(hasFocus);
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            ApplicationPause?.Invoke(pauseStatus);
        }

        private void OnApplicationQuit()
        {
            ApplicationQuit?.Invoke();
        }
    }
}