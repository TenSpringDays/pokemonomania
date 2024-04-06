using System.Collections.Generic;
using System.Linq;
using Pokemonomania.Data;
using Pokemonomania.Services;
using Pokemonomania.StaticData;
using UnityEngine;
using UnityEngine.Pool;


namespace Pokemonomania.Pokemons
{

    public class PokemonFactory
    {
        private readonly GameSceneData _gameSceneData;
        private readonly GameResourcesData _gameResourcesData;

        private ObjectPool<Pokemon>[] _pools;

        public PokemonFactory(GameSceneData gameSceneData,
            GameResourcesData gameResourcesData)
        {
            _gameSceneData = gameSceneData;
            _gameResourcesData = gameResourcesData;
        }

        public Pokemon Create(int id)
        {
            _pools ??= CreatePools(); 
            return _pools[id].Get();
        }

        public void Destroy(Pokemon pokemon)
        {
            _pools[pokemon.Id].Release(pokemon);
        }

        private ObjectPool<Pokemon>[] CreatePools()
        {
            var sceneData = _gameSceneData;
            
            int maxId = sceneData.Pokemons.Max(x => x);
            var pools = new ObjectPool<Pokemon>[maxId + 1];

            foreach (var pokemonId in sceneData.Pokemons)
            {
                var prefab = _gameResourcesData.Pokemons.First(x => x.Id == pokemonId);
                var pool = CreatePokemonObjectPool(prefab);
                pools[pokemonId] = pool;
            }

            return pools;
        }

        private static ObjectPool<Pokemon> CreatePokemonObjectPool(Pokemon prefab)
        {
            var root = new GameObject($"gen:PokemonFactory<{prefab.name}>").transform;
            
            var pool = new ObjectPool<Pokemon>(
                createFunc: () => Object.Instantiate(prefab, root),
                actionOnGet: x => x.gameObject.SetActive(true),
                actionOnRelease: x => x.gameObject.SetActive(false),
                actionOnDestroy: x => Object.Destroy(x.gameObject),
                defaultCapacity: 8);

            return pool;
        }

    }
}