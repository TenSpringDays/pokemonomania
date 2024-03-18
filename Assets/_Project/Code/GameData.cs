using UnityEngine;

namespace StoneBreaker
{
    [CreateAssetMenu(menuName = "StoneBreaker/" + nameof(GameMetaData))]
    public class GameMetaData : ScriptableObject
    {
        [SerializeField] private int _maxCombo;
        [SerializeField] private int _maxScore;

        public int MaxScore
        {
            get => _maxScore;
            set => _maxScore = value;
        }

        public int MaxCombo
        {
            get => _maxCombo;
            set => _maxCombo = value;
        }
    }


}
