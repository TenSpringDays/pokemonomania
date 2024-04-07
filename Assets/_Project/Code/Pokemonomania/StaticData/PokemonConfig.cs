using UnityEngine;


namespace Pokemonomania.StaticData
{
    [CreateAssetMenu(menuName = "Pokemonomania/PokemonConfig")]
    public sealed class PokemonConfig : ScriptableObject
    {
        [SerializeField] private Pokemon _view;
        [SerializeField] private int _minSequence = 3;
        [SerializeField] private int _maxSequence = 15;
        [SerializeField] private int _scorePerCatch = 10;

        public Pokemon View => _view;
        public int MinSequence => _minSequence;
        public int MaxSequence => _maxSequence;
        public int ScorePerCatch => _scorePerCatch;
    }
}