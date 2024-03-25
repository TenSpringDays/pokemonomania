using UnityEngine;


namespace StoneBreaker
{
    public class QuatTests : MonoBehaviour
    {
        [SerializeField] private float _angle;
        [SerializeField] private Vector2 _direction;
        [SerializeField] private Vector2 _angleSpread = new(-30f, 30f);

        [ContextMenu("Random angle")]
        private void RandomAngle()
        {
            _angle = Random.Range(_angleSpread.x, _angleSpread.y);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.color = Color.yellow;
            
            var dir = Quaternion.AngleAxis(_angle, Vector3.forward) * _direction;
            Gizmos.DrawRay(Vector3.zero, dir);
        }
    }
}