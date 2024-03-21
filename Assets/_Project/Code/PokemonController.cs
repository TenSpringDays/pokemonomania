using UnityEngine;


namespace StoneBreaker
{
    public class PokemonController : MonoBehaviour
    {
        [SerializeField] private PokemonControllerConfig _config;
        [SerializeField] private Pokemon[] _prefabs;

        private Pokemon[] _pool;
        private int _spawnedCount;
        private int _spawnId;
        private int _remainSequence;
        private float _position;

        private void OnEnable()
        {
            _pool = new Pokemon[_config.MaxItems];

            for (int i = 0; i < _pool.Length; i++)
            {
                _pool[i] = Instantiate(_prefabs[0], transform);
                _pool[i].gameObject.SetActive(false);
            }

            _position = _config.ItemHeight * _config.MaxItems;
            
            UpdateCurrentSpawnSequence();
        }

        private void Update()
        {
            if (_spawnedCount < _pool.Length)
                Spawn();
            
            for (int i = 0; i < _spawnedCount; i++)
            {
                float y = _position - _config.ItemHeight * i;
                _pool[i].transform.localPosition = new Vector3(0, y, 0);
            }
            
            if (Input.GetKeyDown(KeyCode.Space))
                Catch();
        }

        private void Catch()
        {
            var first = _pool[0];
            
            for (int i = 1; i < _spawnedCount; i++)
                _pool[i - 1] = _pool[i];

            first.gameObject.SetActive(false);
            _pool[_spawnedCount - 1] = first;
            
            _spawnedCount--;
        }

        private void Spawn()
        {
            ref var obj = ref _pool[_spawnedCount++];
            obj.gameObject.SetActive(true);
            obj.Imitate(_prefabs[_spawnId]);
            
            if (--_remainSequence <= 0)
                UpdateCurrentSpawnSequence();
        }
        
        private void UpdateCurrentSpawnSequence()
        {
            var range = _config.SpawnSequenceRange;
            _remainSequence = Random.Range(range.x, range.y);
            int nextId = (_spawnId + Random.Range(1, _prefabs.Length)) % _prefabs.Length;
            _spawnId = nextId;
            
            Debug.Log($"Update sequence: {_remainSequence}; id: {_spawnId}");
        }
    }
}