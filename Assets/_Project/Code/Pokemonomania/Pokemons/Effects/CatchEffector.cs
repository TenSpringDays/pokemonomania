using System;
using UnityEngine;


namespace Pokemonomania.Effects
{

    public abstract class CatchEffector : MonoBehaviour
    {
        protected Transform TargetAnchor { get; private set; }
        
        public void Construct(Transform targetAnchor)
        {
            TargetAnchor = targetAnchor;
        }
        
        public abstract void Prepare();

        public abstract bool Tick();
    }
}