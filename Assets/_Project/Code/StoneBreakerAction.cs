using UnityEngine;
using UnityEngine.Serialization;


public class StoneBreakerAction : MonoBehaviour
{
    [FormerlySerializedAs("stoneBreakerType")] [SerializeField] private StoneType.idType _stoneBreakerType;

    public void BreakingTheStone()
    {
        if (_stoneBreakerType == StoneManager.instance.activeStone[0].getStoneType())
        {
            StoneManager.instance.BreakingStone();
            ScoreManager.instance.addScore();
            ComboManager.instance.addCombo(_stoneBreakerType);
        }
        else
        {
            ComboManager.instance.dropCombo();
            FailedMovementManager.instance.setFMBVisible(true);
        }
    }
}