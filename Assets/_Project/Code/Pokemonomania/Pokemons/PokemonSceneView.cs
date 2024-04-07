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
        [SerializeField] private PokemonFactoryConfig _config;

        private float _position;

        private PokemonSequence _sequence;
        private PokemonViewFactory _factory;
        private (int, Pokemon) _first;

        [Inject]
        public void Constructor(
            PokemonSequence sequence,
            GameResourcesConfig gameResourcesConfig,
            GameSceneData gameSceneData)
        {
            _sequence = sequence;
            _factory = new PokemonViewFactory(gameResourcesConfig, gameSceneData);
        }

        private float TopPosition => _config.ItemHeight * _config.MaxItems - _config.ItemHeight * 0.5f;
        private float BottomPosition => _config.ItemHeight * 0.5f;
        

        public event UnityAction<int, Pokemon> Catched;
        

        private void Start()
        {
            _position = TopPosition;
        }

        private void Update()
        {
            _factory.ReleaseAll();

            float offset = 0f;

            (int, Pokemon) newFirst = default; 

            for (int i = 0; i < _sequence.Count; i++)
            {
                Pokemon pokemon = _factory.Get(_sequence[i]);
                var trans = pokemon.transform;
                trans.localPosition = new Vector3(0f, offset, 0f);
                offset += trans.localScale.y;

                if (i == 0)
                    newFirst = (_sequence[i], pokemon);
            }
            
            if (!ReferenceEquals(_first.Item2, newFirst.Item2))
                Catched?.Invoke(_first.Item1, _first.Item2);

            _first = newFirst;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(new Vector3(0, _position, 0), Vector3.right);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(new Vector3(0, BottomPosition, 0), Vector3.right);
            Gizmos.DrawWireCube(new Vector3(0, TopPosition, 0), Vector3.right);
        }
    }
}