using System;
using System.Collections.Generic;
using Pokemonomania.Data;
using Pokemonomania.Effects;
using Pokemonomania.Services;
using Pokemonomania.StaticData;
using UnityEngine;
using VContainer;


namespace Pokemonomania
{
    public class CatchEffectController : MonoBehaviour
    {
        [SerializeField] private Transform _effectRoot;
        [SerializeField] private Transform[] _catchAnchors;
        private readonly HashSet<CatchEffector> _effectors = new();
        private readonly Stack<CatchEffector> _tempBuffer = new();
        private GameResourcesData _gameResourcesData;
        private PokemonFactory _factory;
        private IDataService _dataSerivce;
        private GameSceneData _gameSceneData;

        [Inject]
        public void Construct(PokemonFactory factory, GameResourcesData gameResourcesData, IDataService dataService)
        {
            _dataSerivce = dataService;
            _factory = factory;
            _gameResourcesData = gameResourcesData;
        }

        private Transform GetAnchor(int id)
        {
            if (_gameSceneData.LeftButton == id)
                return _catchAnchors[0];

            if (_gameSceneData.RightButton == id)
                return _catchAnchors[1];

            if (_gameSceneData.SpectialButton == id)
                return _catchAnchors[2];

            throw new Exception("invalid id and scene data");
        }
        
        private void Start()
        {
            _gameSceneData = _dataSerivce.Load<GameSceneData>();
            _factory.Catched += OnCatched;
        }

        private void OnDestroy()
        {
            _factory.Catched -= OnCatched;
        }

        private void OnCatched(Pokemon obj)
        {
            var newPokemon = Instantiate(
                _gameResourcesData.Pokemons[obj.Id],
                obj.transform.position,
                obj.transform.rotation,
                _effectRoot
            );

            var effector = newPokemon.Effector;
            effector.Construct(GetAnchor(obj.Id));
            effector.Prepare();
            _effectors.Add(effector);
        }

        private void Update()
        {
            foreach (var effector in _effectors)
            {
                if (!effector.Tick())
                    _tempBuffer.Push(effector);
            }

            while (_tempBuffer.TryPop(out var del))
            {
                _effectors.Remove(del);
                Destroy(del.gameObject);
            }
        }
    }
}