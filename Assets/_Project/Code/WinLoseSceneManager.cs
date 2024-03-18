using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinLoseSceneManager : MonoBehaviour {

    [SerializeField]
    GameObject HighScoreText;
    [SerializeField]
    GameObject ScoreText;
    [SerializeField]
    GameObject ComboText;

    [SerializeField]
    string HighScoreStatus1;
    [SerializeField]
    string HighScoreStatus2;

	void Start () {
		
	}
	
	void Update () {
        if (ScoreManager.staticScore >= PlayerPrefs.GetInt("HighScore")) 
            HighScoreText.GetComponent<Text>().text = HighScoreStatus1; 
        else 
            HighScoreText.GetComponent<Text>().text = HighScoreStatus2; 

        ScoreText.GetComponent<Text>().text = "score : " + ScoreManager.staticScore;
        ComboText.GetComponent<Text>().text = "combo : " + ComboManager.staticTotalCombo;
	}
}
