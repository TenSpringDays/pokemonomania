using UnityEngine;


namespace Pokemonomania
{
    [CreateAssetMenu(menuName = "Pokemonomania/Keyboard Config")]
    public class KeyboardConfig : ScriptableObject
    {
        public KeyCode[][] Keys = new KeyCode[][]
        {
            new KeyCode[] { KeyCode.A, KeyCode.LeftArrow },
            new KeyCode[] { KeyCode.D, KeyCode.RightArrow },
            new KeyCode[] { KeyCode.W, KeyCode.UpArrow },
        };

        public float RepeatDelay = 0.1f;
    }
}