using System.Collections.Generic;
using System.Linq;
using Pokemonomania.Data;
using Pokemonomania.StaticData;
using UnityEngine;
using UnityEngine.Pool;


namespace Pokemonomania.Pokemons
{
    public class PokemonViewFactory
    {
        private readonly GameSceneData _gameSceneData;
        private readonly GameResourcesConfig _gameResourcesConfig;

        private readonly List<(int, Pokemon)> _instanced = new();
        private ObjectPool<Pokemon>[] _pools;

        public PokemonViewFactory(
            GameResourcesConfig gameResourcesConfig,
            GameSceneData gameSceneData)
        {
            _gameSceneData = gameSceneData;
            _gameResourcesConfig = gameResourcesConfig;
        }

        public Pokemon Get(int id)
        {
            _pools ??= CreatePools();
            var pokemon = _pools[id].Get();
            _instanced.Add((id, pokemon));
            return pokemon;
        }

        public void ReleaseAll()
        {
            for (int i = 0; i < _instanced.Count; i++)
            {
                (int id, Pokemon pokemon) = _instanced[i];
                _pools[id].Release(pokemon);
            }
        }

        private ObjectPool<Pokemon>[] CreatePools()
        {
            var sceneData = _gameSceneData;
            
            int maxId = sceneData.Pokemons.Max(x => x);
            var pools = new ObjectPool<Pokemon>[maxId + 1];

            foreach (var pokemonId in sceneData.Pokemons)
            {
                var prefab = _gameResourcesConfig.Pokemons[pokemonId].View;
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