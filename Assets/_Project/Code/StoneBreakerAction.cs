using UnityEngine;
using UnityEngine.Serialization;


namespace StoneBreaker
{
    public class StoneBreakerAction : MonoBehaviour
    {
        [FormerlySerializedAs("_stoneBreakerType")] [SerializeField] private PokemonType pokemonBreakerType;

        public void BreakingTheStone()
        {
            if (pokemonBreakerType == StoneManager.Instance.ActivePokemon.PokemonType)
            {
                StoneManager.Instance.BreakingStone();
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