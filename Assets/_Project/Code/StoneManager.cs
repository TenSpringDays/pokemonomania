using System;
using System.Collections.Generic;
using StoneBreaker.Infrastructure;
using UnityEngine;
using Random = UnityEngine.Random;


namespace StoneBreaker
{
    public class StoneManager : Singleton<StoneManager>
    {
        [SerializeField] private Vector2Int _spawnSequenceRange = new Vector2Int(5, 20);
        [SerializeField] private int maximumFilling;
        [SerializeField] private int idGameLevel;
        [SerializeField] private int countAlreadyCreatedStone;

        private IReadOnlyList<GameObject> _stonesInThisLevel;

        private bool _isStillCreatingStone;
        private Queue<Pokemon> _stones;
        private Pokemon _lastAdded;
        private int _currentSpawnId;
        private int _remainsToCreate;

        public Pokemon ActivePokemon => _stones.Peek();

        private void OnValidate()
        {
            var r = _spawnSequenceRange;
            int x = Mathf.Max(0, r.x);
            int y = Mathf.Max(r.x, r.y);
            _spawnSequenceRange = new Vector2Int(x, y);
        }

        private void OnEnable()
        {
            InGameLevelManager.WaitInstance(instance =>
            {
                countAlreadyCreatedStone = 0;
                _isStillCreatingStone = false;
                _stones = new Queue<Pokemon>(maximumFilling);
                _stonesInThisLevel = instance.GameLevels[idGameLevel].StonesInThislevel;
            });
        }

        public void Update()
        {
            if (!_isStillCreatingStone && _stones.Count < maximumFilling)
                FullSpawningStone();
        }

        private void FullSpawningStone()
        {
            _isStillCreatingStone = true;

            SwapnPokemon();
            countAlreadyCreatedStone += 1;
            _isStillCreatingStone = false;
        }

        private void SwapnPokemon()
        {
            if (_remainsToCreate <= 0)
            {
                _remainsToCreate = Random.Range(_spawnSequenceRange.x, _spawnSequenceRange.y);
                _currentSpawnId = (_currentSpawnId + 1) % _stonesInThisLevel.Count;
            }
            
            _remainsToCreate--;
            
            float yPosition = 2f;

            if (!ReferenceEquals(_lastAdded, null))
            {
                yPosition = _lastAdded.transform.position.y + 1f;
                yPosition = Mathf.Min(yPosition, maximumFilling * 1.0f);
            }

            var newPos = new Vector3(0, yPosition, -1f);
            var prefab = _stonesInThisLevel[_currentSpawnId];
            var stoneGO = Instantiate(prefab, newPos, Quaternion.identity);
            var stone = stoneGO.GetComponent<Pokemon>();
            _stones.Enqueue(stone);
            _lastAdded = stone;
        }

        public void BreakingStone()
        {
            if (_stones.Count == 0)
                return;

            var toDel = _stones.Dequeue();
            if (ReferenceEquals(toDel, _lastAdded))
                _lastAdded = null;
            
            Destroy(toDel.gameObject);
        }

        private void OnDrawGizmos()
        {
            if (ReferenceEquals(_lastAdded, null))
                return;
            
            var transform1 = _lastAdded.transform;
            Gizmos.matrix = transform1.localToWorldMatrix;
            Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        }
    }
}