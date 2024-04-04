using UnityEngine;


namespace Pokemonomania
{
    [CreateAssetMenu(menuName = "StoneBreaker/CatchEffectConfig")]
    public class CatchEffectConfig : ScriptableObject
    {
        [SerializeField] private Vector2 _initDirectionSpread = new (-60f, 60f);
        [SerializeField] private float _movementMinSpeed = 3f;
        [SerializeField] private float _movementMaxSpeed = 15f;
        [SerializeField] private float _rotationSpeed = 720f;
        [SerializeField] private float _endThreshold = 0.3f;

        public Vector2 InitDirectionSpread => _initDirectionSpread;
        public float RotationSpeed => _rotationSpeed;
        public float MovementMinSpeed => _movementMinSpeed;
        public float MovementMaxSpeed => _movementMaxSpeed;
        public float EndThreshold => _endThreshold;
    }
}