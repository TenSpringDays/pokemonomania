using UnityEngine;
using UnityEngine.Serialization;


namespace StoneBreaker
{
    public class Pokemon : MonoBehaviour
    {
        [FormerlySerializedAs("_stoneType")] [SerializeField] private PokemonType pokemonType;
        
        public PokemonType PokemonType => pokemonType;
    }
}