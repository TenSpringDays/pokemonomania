using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour {

    public static TimerManager instance;

    [SerializeField]
    int maxTime;
    [SerializeField]
    int decreasingSpeed = 1;
    [SerializeField]
    float time;

    [SerializeField]
    GameObject timerFillObject;

    [SerializeField]
    GameObject timerText;
    [SerializeField]
    string additionalText;

    void Awake()
    {
        instance = this;
    }

	void Start () {
        time = maxTime;
        updateTimerText();
	}
	
	void Update () {
        time -= (decreasingSpeed * Time.deltaTime);
        updateTimerText();
        updateTimerFillObject();

        if (time <= 0f)
        {
            WinLoseManager.instance.changeState(WinLoseManager.WinloseState.isGameOver);
        }
    }

    void updateTimerText()
    {
        timerText.GetComponent<Text>().text = additionalText + time.ToString("0.00");
    }

    void updateTimerFillObject()
    {
        timerFillObject.GetComponent<Image>().fillAmount = time / maxTime;
    }
}
