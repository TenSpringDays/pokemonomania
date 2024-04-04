using Pokemonomania.Hud;
using UnityEngine;


namespace Pokemonomania.StaticData
{
    [CreateAssetMenu(menuName = "Pokemonomania/GameResourcesData")]
    public class GameResourcesData : ScriptableObject
    {
        public CatchButton[] Buttons;
        public Pokemon[] Pokemons;
    }
}