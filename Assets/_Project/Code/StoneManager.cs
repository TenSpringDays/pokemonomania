using System.Collections.Generic;
using StoneBreaker.Infrastructure;
using UnityEngine;


namespace StoneBreaker
{
    public class StoneManager : Singleton<StoneManager>
    {
        [SerializeField] private Vector2Int _spawnSequenceRange = new Vector2Int(5, 20);
        [SerializeField] private int maximumFilling;
        [SerializeField] private int idGameLevel;
        [SerializeField] private int countAlreadyCreatedStone;

        public GameObject[] StoneInThisLevel;

        private bool _isStillCreatingStone;
        private Queue<Stone> _stones;
        private Stone _lastAdded;
        private int _currentSpawnId;
        private int _remainsToCreate;

        public Stone ActiveStone => _stones.Peek();

        private void OnValidate()
        {
            var r = _spawnSequenceRange;
            int x = Mathf.Max(0, r.x);
            int y = Mathf.Max(r.x, r.y);
            _spawnSequenceRange = new Vector2Int(x, y);
        }

        private void OnEnable()
        {
            countAlreadyCreatedStone = 0;
            _isStillCreatingStone = false;
            _stones = new Queue<Stone>(maximumFilling);

            StoneInThisLevel = InGameLevelManager.instance.gameLevel[idGameLevel].StoneInThisLevel;
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
                _currentSpawnId = (_currentSpawnId + 1) % StoneInThisLevel.Length;
            }
            
            _remainsToCreate--;
            
            float yPosition = 6f;
            if (!ReferenceEquals(_lastAdded, null))
                yPosition = _lastAdded.transform.position.y + 2f;
                
            var newPos = new Vector3(0, yPosition, -1f);
            var prefab = StoneInThisLevel[_currentSpawnId];
            var stoneGO = Instantiate(prefab, newPos, Quaternion.identity);
            var stone = stoneGO.GetComponent<Stone>();
            stone.StoneID = _stones.Count;
            _stones.Enqueue(stone);
            _lastAdded = stone;
        }

        public void BreakingStone()
        {
            if (_stones.Count == 0)
                return;

            Destroy(_stones.Dequeue().gameObject);

            if (_stones.Count == 0)
                _lastAdded = null;
        }
    }
}