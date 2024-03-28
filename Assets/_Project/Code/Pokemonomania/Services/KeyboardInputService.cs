using System;
using System.Collections.Generic;
using Pokemonomania.Hud;
using UnityEngine;


namespace Pokemonomania
{
    public class KeyboardInputService : IInputService
    {
        private struct EventPair
        {
            public readonly int Index;
            public readonly KeyCode Key;
            public float PressTime;
            public bool Pressed;

            public EventPair(int index, KeyCode key)
            {
                Index = index;
                Key = key;
                PressTime = 0f;
                Pressed = false;
            }
        }


        private readonly KeyboardConfig _config;
        private EventPair[] _eventPairs;

        public KeyboardInputService(KeyboardConfig config)
        {
            _config = config;
        }
        
        public event Action<int> Pressed; 

        public void Enable(int maxInputIndexes)
        {
            var arr = ConfigKeysToEventPairs(_config.Keys);
            _eventPairs = arr;
        }

        public void Disable()
        {
            _eventPairs = null;
        }

        public void Tick(float delta)
        {
            float time = Time.realtimeSinceStartup;
            UpdatePressRepeat(time);
            UpdateButtonDown(time);
            UpdateButtonUp();
        }

        public void LostFocus()
        {
            for (int i = 0; i < _eventPairs.Length; i++)
            {
                ref var item = ref _eventPairs[i];

                if (item.Pressed && Input.GetKeyUp(item.Key))
                {
                    item.Pressed = false;
                    item.PressTime = 0f;
                }
            }
        }

        private void UpdatePressRepeat(float time)
        {
            float delayed = time - _config.RepeatDelay;
            
            for (int i = 0; i < _eventPairs.Length; i++)
            {
                ref var item = ref _eventPairs[i];

                if (item.Pressed && item.PressTime < delayed)
                {
                    item.PressTime += _config.RepeatDelay;
                    Pressed?.Invoke(item.Index);
                } 
            }
        }

        private void UpdateButtonDown(float time)
        {
            for (int i = 0; i < _eventPairs.Length; i++)
            {
                ref var item = ref _eventPairs[i];

                if (Input.GetKeyDown(item.Key))
                {
                    item.Pressed = true;
                    item.PressTime = time;
                    Pressed?.Invoke(item.Index);
                }
            }
        }

        private void UpdateButtonUp()
        {
            for (int i = 0; i < _eventPairs.Length; i++)
            {
                ref var item = ref _eventPairs[i];

                if (item.Pressed && Input.GetKeyUp(item.Key))
                {
                    item.Pressed = false;
                    item.PressTime = 0f;
                }
            }
        }

        private static EventPair[] ConfigKeysToEventPairs(KeyCode[][] keys)
        {
            var list = new List<EventPair>();

            for (int i = 0; i < keys.Length; i++)
            {
                for (int j = 0; j < keys[i].Length; j++)
                {
                    list.Add(new EventPair(i, keys[i][j]));
                }
            }

            return list.ToArray();
        }
    }
}