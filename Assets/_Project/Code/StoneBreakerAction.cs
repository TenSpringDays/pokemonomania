using StoneBreaker;
using UnityEngine;


namespace StoneBreaker
{
    public class StoneBreakerAction : MonoBehaviour
    {
        [SerializeField] private StoneType.idType _stoneBreakerType;

        public void BreakingTheStone()
        {
            if (_stoneBreakerType == StoneManager.instance.activeStone[0].getStoneType())
            {
                StoneManager.instance.BreakingStone();
                ScoreService.Instance.AddScore();
                ComboManager.instance.addCombo(_stoneBreakerType);
            }
            else
            {
                ComboManager.instance.dropCombo();
                FailedMovementManager.instance.setFMBVisible(true);
            }
        }
    }
}