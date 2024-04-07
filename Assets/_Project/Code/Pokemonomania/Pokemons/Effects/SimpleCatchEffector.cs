using UnityEngine;


namespace Pokemonomania.Effects
{
    public class SimpleCatchEffector : CatchEffector
    {
        [SerializeField] private SimpleCatchEffectConfig _config;

        private float _baseDistance;

        public override void Prepare()
        {
            _baseDistance = Vector3.Distance(transform.position, TargetAnchor.position);
        }

        public override bool Tick()
        {
            var trans = transform;
            var cur = trans.position;
            var target = TargetAnchor.position;

            var direction = target - cur;
            var distance = direction.magnitude;

            if (Mathf.Abs(distance) <= _config.EndThreshold)
                return false;

            var vel = (direction / distance) * (_config.MovementMaxSpeed * Time.deltaTime);
            var nextPos = cur + vel;
            trans.position = nextPos;

            float rotSpeed = _config.RotationSpeed * Time.deltaTime;
            var rot = Quaternion.AngleAxis(rotSpeed, Vector3.forward);
            trans.localRotation = rot * trans.localRotation;

            var scale = Vector3.one * (distance / _baseDistance);
            trans.localScale = scale;

            return true;
        }
    }
}