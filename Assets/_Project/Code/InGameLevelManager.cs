using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameLevelManager : MonoBehaviour {

    public static InGameLevelManager instance;

    [System.Serializable]
    public class GameLevel
    {
        public GameObject[] StoneInThisLevel;
    }

    public GameLevel[] gameLevel;

    void Awake()
    {
        instance = this;
    }
}
