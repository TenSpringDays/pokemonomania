using System.Collections.Generic;
using Pokemonomania.Data;
using Pokemonomania.Model;
using Pokemonomania.Pokemons;
using Pokemonomania.StaticData;
using UnityEngine;
using UnityEngine.Events;
using VContainer;


namespace Pokemonomania
{
    public class PokemonSceneView : MonoBehaviour
    {
        [SerializeField] private Transform _lowerAnchor;
        [SerializeField] private PokemonFactoryConfig _config;


        private readonly Queue<Pokemon> _pokemons = new();
        private PokemonViewFactory _factory;
        private float _position;

        [Inject]
        public void Constructor(
            GameResourcesConfig gameResourcesConfig,
            GameSceneData gameSceneData)
        {
            _factory = new PokemonViewFactory(_lowerAnchor, gameResourcesConfig, gameSceneData);
        }

        public event UnityAction<Pokemon> Catched;

        private void Start()
        {
        }

        public Pokemon PushBack(int id)
        {
            var pokemon = _factory.Get(id);
            _pokemons.Enqueue(pokemon);
            return pokemon;
        }

        public Pokemon PopFirst()
        {
            var pokemon = _pokemons.Dequeue();
            _factory.Release(pokemon);
            Catched?.Invoke(pokemon);
            _position += pokemon.transform.localScale.y;
            return pokemon;
        }

        private void Update()
        {
            UpdatePosition();
            UpdateInstancedPokemonPositions();
        }

        private void UpdatePosition()
        {
            float delta = _config.FallSpeed * Mathf.Pow(_position, _config.PositionPower) * Time.deltaTime;
            _position = Mathf.Max(_position - delta, 0f);
        }

        private void UpdateInstancedPokemonPositions()
        {
            float offset = _position;
            
            foreach (Pokemon pokemon in _pokemons)
            {
                var trans = pokemon.transform;
                trans.localPosition = new Vector3(0f, offset, 0f);
                offset += trans.localScale.y;
            }
        }
    }
}