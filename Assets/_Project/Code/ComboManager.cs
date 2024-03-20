using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboManager : MonoBehaviour
{
    public static ComboManager instance;

    [SerializeField]
    int leftStoneCombo;
    [SerializeField]
    int rightStoneCombo;
    [SerializeField]
    int totalCombo;
    [SerializeField]
    int maximumComboInOnePlay;

    [SerializeField]
    float maxComboRangeTime;
    float comboRangeTime;

    [SerializeField]
    GameObject totalComboText;
    [SerializeField]
    string additionalTotalComboText;

    public static int staticTotalCombo;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        resetCombo();
        maximumComboInOnePlay = 0;
        staticTotalCombo = maximumComboInOnePlay;
        comboRangeTime = 0;
    }

    void Update()
    {
        if (comboRangeTime > 0)
        {
            comboRangeTime -= Time.deltaTime;
        }
        else
        {
            updateMaximumComboInOnePlay(totalCombo);
            resetCombo();
        }
    }

    public void addCombo(StoneType.idType stoneType)
    {
        totalCombo += 1;
        if (stoneType == StoneType.idType.left)
            leftStoneCombo += 1;
        else if (stoneType == StoneType.idType.right)
            rightStoneCombo += 1;

        comboRangeTime = maxComboRangeTime;
        updateComboText();
    }

    public void dropCombo()
    {
        comboRangeTime = 0;
        updateMaximumComboInOnePlay(totalCombo);
        resetCombo();
    }

    public int getCombo()
    {
        return totalCombo;
    }

    void resetCombo()
    {
        leftStoneCombo = 0;
        rightStoneCombo = 0;
        totalCombo = 0;
        updateComboText();
    }

    void updateComboText()
    {
        totalComboText.GetComponent<Text>().text = additionalTotalComboText + totalCombo;
    }

    void updateMaximumComboInOnePlay(int newTotalCombo)
    {
        if (newTotalCombo > maximumComboInOnePlay)
        {
            maximumComboInOnePlay = newTotalCombo;
            staticTotalCombo = maximumComboInOnePlay;
        }
    }
}