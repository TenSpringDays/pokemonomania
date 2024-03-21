using System.Collections.Generic;
using StoneBreaker.Infrastructure;
using UnityEngine;
using UnityEngine.Serialization;


namespace StoneBreaker
{
    public class InGameLevelManager : Singleton<InGameLevelManager>
    {
        [System.Serializable]
        public class GameLevel
        {
            [FormerlySerializedAs("StoneInThisLevel")] [SerializeField] private GameObject[] _stoneInThisLevel;

            public GameObject[] StonesInThislevel => _stoneInThisLevel;
        }


        [FormerlySerializedAs("gameLevel")] [SerializeField] private GameLevel[] _gameLevel;

        public IReadOnlyList<GameLevel> GameLevels => _gameLevel;
    }
}