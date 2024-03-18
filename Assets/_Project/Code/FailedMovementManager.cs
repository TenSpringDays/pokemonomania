using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailedMovementManager : MonoBehaviour {

    public static FailedMovementManager instance;

    [SerializeField]
    GameObject failedMovementBackground;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        setFMBVisible(false);
    }

    public void setFMBVisible(bool state)
    {
        failedMovementBackground.SetActive(state);
    }
}
