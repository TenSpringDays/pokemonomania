using UnityEngine;


namespace StoneBreaker
{
    [System.Serializable]
    public class ScoreMultiplyStage
    {
        [field:SerializeField] public int TotalCombo { get; private set; }
        [field:SerializeField] public float Multiplying { get; private set; }
        [field:SerializeField] public Color ComboTextColor { get; private set; }
    }
}