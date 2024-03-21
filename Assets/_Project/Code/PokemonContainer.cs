using System;
using UnityEngine;


namespace StoneBreaker
{
    public class PokemonContainer
    {
        private readonly PokemonControllerConfig _config;
        private float _position;
        private int _count;


        public PokemonContainer(PokemonControllerConfig config)
        {
            _config = config;
        }

        public int Count => _count;

        public int Capacity => _config.MaxItems;
        
        public void Tick(float delta)
        {
            float pos = _position;
            float maxHeight = _config.ItemHeight * _config.MaxItems;

            float acceleration = Mathf.Lerp(_config.MaxSpeed, _config.MinSpeed, pos / maxHeight);
            pos += acceleration * delta;
            pos = Mathf.Min(pos, maxHeight);
            
            _count = CalcCount(pos);
            _position = pos;
        }

        public void Pop()
        {
            if (_count == 0)
                return;

            _position -= _config.ItemHeight;
            _count -= 1;
        }

        public float GetPosition(int id)
        {
            return id * _config.ItemHeight + _position - _count * _config.ItemHeight;
        }

        private int CalcCount(float pos)
        {
            return Mathf.CeilToInt(pos / _config.ItemHeight);
        }
    }
}