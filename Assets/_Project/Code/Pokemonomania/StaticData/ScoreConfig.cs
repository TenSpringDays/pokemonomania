using UnityEngine;


namespace Pokemonomania
{
    [CreateAssetMenu(menuName = "Pokemonomania/Score Config")]
    public class ScoreConfig : ScriptableObject
    {
        public int ScorePerCatch = 20;
        public float DropComboDelay = 0.8f;
        public ComboStage[] MultiplyStages =
        {
            new ComboStage()
            {
                RequiredCombo = 0,
                Multiplier = 0.5f,
                TextColor = Color.gray,
            },
            new ComboStage()
            {
                RequiredCombo = 10,
                Multiplier = 1.0f,
                TextColor = Color.white,
            },
            new ComboStage()
            {
                RequiredCombo = 20,
                Multiplier = 1.5f,
                TextColor = Color.green,
            },
            new ComboStage()
            {
                RequiredCombo = 40,
                Multiplier = 3f,
                TextColor = Color.magenta,
            },
        };
    }
}