using UnityEngine;


namespace StoneBreaker
{
    public class PokemonController : MonoBehaviour
    {
        [SerializeField] private PokemonControllerConfig _config;
        [SerializeField] private Pokemon[] _prefabs;

        private Pokemon[] _pool = System.Array.Empty<Pokemon>();
        private int _spawnedCount;
        private int _spawnId;
        private int _remainSequence;
        private float _position;

        private float TopPosition => _config.ItemHeight * _config.MaxItems - _config.ItemHeight * 0.5f;
        private float BottomPosition => _config.ItemHeight * 0.5f;
        
        public int CurrentId { get; private set; }

        private void OnEnable()
        {
            UpdateCurrentSpawnSequence();
            CurrentId = _spawnId;
            _position = TopPosition;
        }

        private void Update()
        {
            while (TrySpawn()) {}
            
            UpdatePoolSize();
            UpdatePosition();
            UpdatePokemons();
        }

        private void UpdatePosition()
        {
            float delta = _config.FallSpeed * Mathf.Pow(_position, _config.PositionPower) * Time.deltaTime;
            _position = Mathf.Max(_position - delta, BottomPosition);
        }

        private void UpdatePokemons()
        {
            float offset = 0;

            for (int i = 0; i < _spawnedCount; i++)
            {
                float y = _position + offset;
                _pool[i].transform.localPosition = new Vector3(0f, y, 0f);
                offset += _config.ItemHeight;
            }
        }

        public void Catch()
        {
            var first = _pool[0];
            
            for (int i = 1; i < _spawnedCount; i++)
                _pool[i - 1] = _pool[i];

            first.gameObject.SetActive(false);
            _pool[_spawnedCount - 1] = first;
            CurrentId = _pool[0].Id;

            _spawnedCount--;
            _position = Mathf.Min(_position + _config.ItemHeight, TopPosition);
        }

        private void UpdatePoolSize()
        {
            if (_pool.Length != _config.MaxItems)
            {
                int oldSize = _pool.Length;
                int newSize = _config.MaxItems;
                _spawnedCount = Mathf.Min(_spawnedCount, newSize);

                for (int i = newSize; i < oldSize; i++)
                {
                    Destroy(_pool[i].gameObject);
                }

                System.Array.Resize(ref _pool, newSize);
                
                for (int i = oldSize; i < newSize; i++)
                {
                    _pool[i] = Instantiate(_prefabs[0], transform);
                    _pool[i].gameObject.SetActive(false);
                }
            }
        }

        private bool TrySpawn()
        {
            if (_spawnedCount == _pool.Length)
                return false;

            ref var pokemon = ref _pool[_spawnedCount++];
            pokemon.gameObject.SetActive(true);
            pokemon.Init(_prefabs[_spawnId], _spawnId);
            
            if (--_remainSequence <= 0)
                UpdateCurrentSpawnSequence();

            return true;
        }

        private void UpdateCurrentSpawnSequence()
        {
            var range = _config.SpawnSequenceRange;
            _remainSequence = Random.Range(range.x, range.y);
            int nextId = (_spawnId + Random.Range(1, _prefabs.Length)) % _prefabs.Length;
            _spawnId = nextId;
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