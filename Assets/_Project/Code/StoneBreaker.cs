using UnityEngine;


public class StoneBreakerAction : MonoBehaviour
{

    [SerializeField] StoneType.idType stoneBreakerType;

    void Start()
    {
    }

    public void BreakingTheStone()
    {
        if (stoneBreakerType == StoneManager.instance.activeStone[0].getStoneType())
        {
            StoneManager.instance.BreakingStone();
            ScoreManager.instance.addScore();
            ComboManager.instance.addCombo(stoneBreakerType);
        }
        else
        {
            ComboManager.instance.dropCombo();
            FailedMovementManager.instance.setFMBVisible(true);
        }
    }
}