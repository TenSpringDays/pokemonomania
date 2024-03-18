using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeManager : MonoBehaviour {

    [SerializeField]
    string NextSceneTarget;

    [SerializeField]
    GameObject HighScoreText;

    void Start()
    {
        HighScoreText.GetComponent<Text>().text = "HIGHSCORE : " + PlayerPrefs.GetInt("HighScore");
    }

    public void goToNextScene()
    {
        SceneManager.LoadScene(NextSceneTarget);
    }
}
