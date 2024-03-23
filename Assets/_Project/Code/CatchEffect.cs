using UnityEngine;


namespace StoneBreaker
{
    public class CatchEffect
    {
        public Pokemon Pokemon;
        public Vector3 Destination;
        public Vector3 Impulse;
        public float Speed;
        public float DistToEnd;


        public void Update(float delta)
        {
            Transform trans = Pokemon.transform;
            Vector3 pos = trans.position;
            Vector3 dir = Destination - pos;
            float dist = dir.magnitude;
            Vector3 vel = (dir / dist) * (Speed * delta);
            vel += Impulse * delta;
            pos += vel;

            trans.position = pos;
            DistToEnd = dist;
        }
    }
}