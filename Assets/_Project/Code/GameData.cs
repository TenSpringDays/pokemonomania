using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoneBreaker
{
    [CreateAssetMenu(fileName = "GameData", menuName = "StoneBreaker/GameData")]
    public class GameData : ScriptableObject
    {
        [SerializeField] private int _maxCombo;
        [SerializeField] private int _maxScore;
    }


}
