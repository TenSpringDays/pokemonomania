using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseManager : MonoBehaviour {

    public static WinLoseManager instance;

    public enum WinloseState
    {
        gameStillRunning,
        isGameOver
    }

    public string sceneTargetAfterWinLose;
    WinloseState winloseState;

    void Awake()
    {
        instance = this;
    }

	void Start () {
        changeState(WinloseState.gameStillRunning);
	}

    void Update()
    {
        checkState(winloseState);
    }

	public void changeState (WinloseState state) {
        winloseState = state;
	}

    void checkState(WinloseState state)
    {
        if (state == WinloseState.isGameOver)
        {
            if (ScoreManager.staticScore > PlayerPrefs.GetInt("HighScore"))
                PlayerPrefs.SetInt("HighScore", ScoreManager.staticScore);
            SceneManager.LoadScene(sceneTargetAfterWinLose);
        }
    }
}
