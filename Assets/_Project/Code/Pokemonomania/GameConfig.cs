using UnityEngine;


namespace Pokemonomania
{
    [CreateAssetMenu(menuName = "Pokemonomania/Game Config")]
    public class ScoreConfig : ScriptableObject
    {
        public int ScorePerCatch = 20;
        public float DropComboDelay = 0.8f;
        public MultiplyStage[] MultiplyStages =
        {
            new MultiplyStage()
            {
                RequiredCombo = 0,
                Multiplier = 0.5f,
                TextColor = Color.gray,
            },
            new MultiplyStage()
            {
                RequiredCombo = 10,
                Multiplier = 1.0f,
                TextColor = Color.white,
            },
            new MultiplyStage()
            {
                RequiredCombo = 20,
                Multiplier = 1.5f,
                TextColor = Color.green,
            },
            new MultiplyStage()
            {
                RequiredCombo = 40,
                Multiplier = 3f,
                TextColor = Color.magenta,
            },
        };

        [System.Serializable]
        public struct MultiplyStage
        {
            public int RequiredCombo;
            public float Multiplier;
            public Color TextColor;
        }
    }
}