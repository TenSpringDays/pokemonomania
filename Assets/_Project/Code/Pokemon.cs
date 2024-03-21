using UnityEngine;


namespace StoneBreaker
{

    public class Pokemon : MonoBehaviour, IImitator<Pokemon>
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private PokemonType _type;

        public PokemonType Type => _type;
        
        public void Imitate(Pokemon other)
        {
            _spriteRenderer.sprite = other._spriteRenderer.sprite;
            _type = other._type;
        }
    }
}