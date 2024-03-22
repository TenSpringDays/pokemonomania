using UnityEngine;


namespace StoneBreaker
{
    public class StoneBreakerAction : MonoBehaviour
    {
        [SerializeField] private PokemonController _controller;
        [SerializeField] private int _breakerId;

        public void BreakingTheStone()
        {
            if (_breakerId == _controller.CurrentId)
            {
                _controller.Catch();
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