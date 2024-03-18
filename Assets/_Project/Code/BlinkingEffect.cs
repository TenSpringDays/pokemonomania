using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingEffect : MonoBehaviour {

    [SerializeField]
    GameObject blinkedText;
    [SerializeField]
    float blinkedTextVisibleTime;
    [SerializeField]
    float blinkedTextUnvisibleTime;
    float time;

    void Start()
    {
        time = blinkedTextVisibleTime;
    }

    void Update()
    {
        textBlinking();
    }

    void textBlinking()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        else
        {
            if (blinkedText.activeInHierarchy)
            {
                time = blinkedTextUnvisibleTime;
                blinkedText.SetActive(false);
            }
            else
            {
                time = blinkedTextVisibleTime;
                blinkedText.SetActive(true);
            }
        }
    }
}
