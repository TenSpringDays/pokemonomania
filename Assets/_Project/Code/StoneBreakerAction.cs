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
                ScoreManager.Instance.AddScore();
                ComboManager.Instance.AddCombo();
            }
            else
            {
                ComboManager.Instance.DropCombo();
                FailedMovementManager.instance.setFMBVisible(true);
            }
        }
    }
}