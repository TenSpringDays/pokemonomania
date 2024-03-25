using System.Collections;
using System.Collections.Generic;
using StoneBreaker;
using UnityEngine;

public class FailedMovementManager : MonoBehaviour {

    public static FailedMovementManager instance;

    [SerializeField]
    GameObject failedMovementBackground;

    [SerializeField] private InputObserver _inputObserver;

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
        _inputObserver.gameObject.SetActive(!state);
    }
}
