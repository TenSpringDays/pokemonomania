using UnityEngine;


namespace StoneBreaker
{

    public class CatchEffect
    {
        private readonly Pokemon _pokemon;
        private CatchEffectConfig _config;
        private Vector3 _destination;
        private Vector3 _direction;
        private float _distToEnd;
        private float _totalDistToEnd;
        private float _rotatorAngle;

        public CatchEffect(Pokemon pokemon)
        {
            _pokemon = pokemon;
        }

        public bool InEnd => _distToEnd < _config.EndThreshold;


        public void Init(Pokemon source, Vector3 destination, CatchEffectConfig config)
        {
            _pokemon.Imitate(source);
            var sourcePos = source.transform.position;
            var vecToEnd = destination - sourcePos;
            var distToEnd = vecToEnd.magnitude;
            var dirToEnd = vecToEnd / distToEnd;
            var angle = Random.Range(config.InitDirectionSpread.x, config.InitDirectionSpread.y);
            var rotatedDir = Quaternion.AngleAxis(angle, Vector3.forward) * dirToEnd;

            var transform = _pokemon.transform;
            transform.position = sourcePos;
            transform.localScale = Vector3.one;
            transform.localRotation = Quaternion.identity;
            
            _config = config;
            _destination = destination;
            _direction = rotatedDir;
            _distToEnd = distToEnd;
            _totalDistToEnd = distToEnd;
            _rotatorAngle = Random.Range(-config.RotationSpeed, config.RotationSpeed);
        }

        public void SetPokemonVisibility(bool state)
        {
            _pokemon.gameObject.SetActive(state);
        }

        public void Update(float delta)
        {
            Transform trans = _pokemon.transform;
            Vector3 pos = trans.position;
            Vector3 oldPos = pos;

            float t = _distToEnd / _totalDistToEnd;
            float speed = Mathf.Lerp(_config.MovementMinSpeed, _config.MovementMaxSpeed, t);
            Vector3 dir = _destination - pos;
            float dist = dir.magnitude;
            Vector3 vel = (dir / dist) * (speed * delta);
            vel += _direction * delta;
            pos += vel;

            _distToEnd = dist;
            trans.position = pos;

            trans.Rotate(Vector3.forward, _rotatorAngle * delta, Space.Self);
            
            trans.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, t);

            Debug.DrawRay(oldPos, _direction, Color.yellow);
            Debug.DrawRay(oldPos, vel / delta, Color.red);
        }
    }
}