using System;
using System.Collections.Generic;
using Pokemonomania.Hud;
using UnityEngine;


namespace Pokemonomania
{
    public class KeyboardInput : IInputService
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
        private readonly Queue<int> _eventsQueue;
        private EventPair[] _eventPairs;

        public KeyboardInput(KeyboardConfig config)
        {
            _config = config;
            _eventsQueue = new Queue<int>(8);
        }
        
        public event Action<int> Pressed; 

        public void Enable(int maxInputIndexes)
        {
            var arr = ConfigKeysToEventPairs(_config.Keys, maxInputIndexes);
            _eventPairs = arr;
        }

        public void Disable()
        {
            LostFocus();
            _eventPairs = null;
            Pressed = null;
        }

        public void Tick(float delta)
        {
            if (_eventPairs != null && _eventPairs.Length > 0)
            {
                float time = Time.realtimeSinceStartup;
                UpdatePressRepeat(time);
                UpdateButtonDown(time);
                UpdateButtonUp();
                InvokeEvent();
            }
        }

        public void LostFocus()
        {
            if (_eventPairs == null)
                return;
            
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

        private void InvokeEvent()
        {
            while (_eventsQueue.TryDequeue(out int index))
                Pressed?.Invoke(index);
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
                    _eventsQueue.Enqueue(item.Index);
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
                    _eventsQueue.Enqueue(item.Index);
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

        private static EventPair[] ConfigKeysToEventPairs(KeyboardConfig.KeyVariants[] keys, int maxInputIndexes)
        {
            int count = Mathf.Min(maxInputIndexes, keys.Length);
            var list = new List<EventPair>();

            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < keys[i].Keys.Length; j++)
                {
                    list.Add(new EventPair(i, keys[i].Keys[j]));
                }
            }

            return list.ToArray();
        }
    }
}