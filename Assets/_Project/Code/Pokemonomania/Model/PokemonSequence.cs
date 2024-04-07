using System.Collections.Generic;
using Pokemonomania.Data;
using Pokemonomania.StaticData;
using UnityEngine;


namespace Pokemonomania.Model
{
    public class PokemonSequence
    {
        private readonly IReadOnlyList<PokemonConfig> _configs;
        private readonly GameSceneData _sceneData;

        private int[] _pool;
        private int _count = 0;
        private int _scenePokemonId = -1;
        private int _remainSequence;

        public PokemonSequence(IReadOnlyList<PokemonConfig> configs,
                               GameSceneData sceneData)
        {
            _configs = configs;
            _sceneData = sceneData;
        }
        
        public int Count => _count;
        public int First => _pool[0];

        public int this[int position] => _pool[position];

        
        public void Initialize()
        {
            _pool = new int[8];
            
            for (int i = 0; i < _pool.Length; i++)
                PushLast();
        }


        public int PopFirst()
        {
            if (_count < 0)
                throw new UnityException("Can't pop from empty sequence");
            
            int first = First;

            for (int i = 1; i < _count; i++)
                _pool[i - 1] = _pool[i];

            _count--;

            return first;
        }

        public int PushLast()
        {
            if (_remainSequence <= 0)
                MoveSequence();

            if (_count >= _pool.Length)
                System.Array.Resize(ref _pool, _count * 2);

            var last = _sceneData.Pokemons[_scenePokemonId];
            _pool[_count++] = last;
            return last;
        }

        private void MoveSequence()
        {
            var len = _sceneData.Pokemons.Length;
            _scenePokemonId = (_scenePokemonId + Random.Range(1, len)) % len;
            int id = _sceneData.Pokemons[_scenePokemonId];
            var config = _configs[id];
            _remainSequence = Random.Range(config.MinSequence, config.MaxSequence + 1);
        }
    }
}