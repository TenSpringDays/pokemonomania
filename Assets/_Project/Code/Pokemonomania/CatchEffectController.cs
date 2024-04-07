using System;
using System.Collections.Generic;
using Pokemonomania.Data;
using Pokemonomania.Effects;
using Pokemonomania.Model;
using Pokemonomania.Services;
using Pokemonomania.StaticData;
using UnityEngine;
using VContainer;


namespace Pokemonomania
{
    public class CatchEffectController : MonoBehaviour
    {
        [SerializeField] private PokemonSceneView _sceneView;
        [SerializeField] private Transform _effectRoot;
        [SerializeField] private Transform[] _catchAnchors;
        private readonly HashSet<CatchEffector> _effectors = new();
        private readonly Stack<CatchEffector> _tempBuffer = new();
        private GameResourcesConfig _gameResourcesConfig;
        private GameSceneData _gameSceneData;

        [Inject]
        public void Construct(
            GameResourcesConfig gameResourcesConfig,
            GameSceneData gameSceneData)
        {
            _gameSceneData = gameSceneData;
            _gameResourcesConfig = gameResourcesConfig;
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
            _sceneView.Catched += OnCatched;
        }

        private void OnDestroy()
        {
            _sceneView.Catched -= OnCatched;
        }

        private void OnCatched(Pokemon view)
        {
            var viewTrans = view.transform;
            
            var newPokemon = Instantiate(
                _gameResourcesConfig.Pokemons[view.Id].View,
                viewTrans.position,
                viewTrans.rotation,
                _effectRoot
            );

            var effector = newPokemon.CatchEffector;
            effector.Construct(GetAnchor(view.Id));
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