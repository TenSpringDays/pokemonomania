using UnityEngine;


namespace Pokemonomania
{
    [CreateAssetMenu(menuName = "Pokemonomania/Keyboard Config")]
    public class KeyboardConfig : ScriptableObject
    {
        [System.Serializable]
        public struct KeyVariants
        {
            public KeyCode[] Keys;
        }


        public KeyVariants[] Keys = new KeyVariants[]
        {
            new KeyVariants { Keys = new KeyCode[] { KeyCode.A, KeyCode.LeftArrow } },
            new KeyVariants { Keys = new KeyCode[] { KeyCode.D, KeyCode.RightArrow } },
            new KeyVariants { Keys = new KeyCode[] { KeyCode.W, KeyCode.UpArrow } },
        };

        public float RepeatDelay = 0.1f;
    }
}