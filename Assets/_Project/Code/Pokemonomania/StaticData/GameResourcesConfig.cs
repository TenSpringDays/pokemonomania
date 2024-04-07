using System.Collections.Generic;
using UnityEngine;


namespace Pokemonomania.StaticData
{
    [CreateAssetMenu(menuName = "Pokemonomania/GameResourcesConfig")]
    public sealed class GameResourcesConfig : ScriptableObject
    {
        // public CatchButton[] Buttons;
        // public Pokemon[] Pokemons;
        [SerializeField] private PokemonConfig[] _pokemons;

        public IReadOnlyList<PokemonConfig> Pokemons => _pokemons;
    }



}