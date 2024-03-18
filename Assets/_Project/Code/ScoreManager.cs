using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [SerializeField]
    int score;
    public int Score
    {
        get
        {
            return score;
        }
    }

    [SerializeField]
    int scorePerBreaking;
    [SerializeField]
    GameObject scoreText;
    [SerializeField]
    string additionalText;

    [SerializeField]
    int destroyedLeftStoneTotal;
    [SerializeField]
    int destroyedRightStoneTotal;
    [SerializeField]
    int destroyedBothStoneTotal;

    [System.Serializable]
    public class ScoreMultiply
    {
        public int totalCombo;
        public int multipliedScore;
        public Color comboTextColor;
    }

    [SerializeField]
    List<ScoreMultiply> scoreMultiply;
    [SerializeField]
    GameObject scoreMultiplyText;
    [SerializeField]
    string additionalScoreMultiplyText;

    public static int staticScore;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        score = 0;
        staticScore = score;
        updateScoreText();
        destroyedLeftStoneTotal = 0;
        destroyedRightStoneTotal = 0;
        destroyedBothStoneTotal = 0; ;
        updateScoreMultiplyText(updateIndexScoreMultiply());
    }

    void FixedUpdate()
    {
        int tempIndex = updateIndexScoreMultiply();
        updateScoreMultiplyText(tempIndex);
        staticScore = score;
    }

    public void addScore(int addedPoint)
    {
        int tempIndex = updateIndexScoreMultiply();
        score += (addedPoint * scoreMultiply[tempIndex].multipliedScore);
        updateScoreText();
    }

    public void addScore()
    {
        int tempIndex = updateIndexScoreMultiply();
        score += (scorePerBreaking * scoreMultiply[tempIndex].multipliedScore);
        updateScoreText();
    }

    void updateScoreText()
    {
        scoreText.GetComponent<Text>().text = additionalText + score;
    }

    int updateIndexScoreMultiply()
    {
        //int multipliedScore = 0;
        int index = 0;
        for (int i = 0; i < scoreMultiply.Count; i++)
        {
            if (ComboManager.instance.getCombo() < scoreMultiply[i].totalCombo & i > 0)
            {
                index = i - 1;
                break;
            }
        }
        return index;
    }

    public void updateScoreMultiplyText(int index)
    {
        scoreMultiplyText.GetComponent<Text>().text = additionalScoreMultiplyText + scoreMultiply[index].multipliedScore;
        scoreMultiplyText.GetComponent<Text>().color = new Color(scoreMultiply[index].comboTextColor.r,
            scoreMultiply[index].comboTextColor.g, scoreMultiply[index].comboTextColor.b);
    }

    void updateDestroyedStoneTotal(StoneType.idType stoneType)
    {
        if (stoneType == StoneType.idType.left)
            destroyedLeftStoneTotal += 1;
        else if (stoneType == StoneType.idType.right)
            destroyedRightStoneTotal += 1;

        destroyedBothStoneTotal += 1;
    }
}