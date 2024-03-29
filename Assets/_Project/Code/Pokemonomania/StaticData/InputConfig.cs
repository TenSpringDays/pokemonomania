using UnityEngine;
using UnityEngine.Serialization;


namespace Pokemonomania
{
    [CreateAssetMenu(menuName = "Pokemonomania/Keyboard Config")]
    public class InputConfig : ScriptableObject
    {
        [System.Serializable]
        public struct KeyVariants
        {
            public KeyCode[] Variants;
        }


        public KeyVariants[] Keys = new KeyVariants[]
        {
            new KeyVariants { Variants = new KeyCode[] { KeyCode.A, KeyCode.LeftArrow } },
            new KeyVariants { Variants = new KeyCode[] { KeyCode.D, KeyCode.RightArrow } },
            new KeyVariants { Variants = new KeyCode[] { KeyCode.W, KeyCode.UpArrow } },
        };

        public float RepeatDelay = 0.15f;
    }
}