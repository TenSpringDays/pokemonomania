using UnityEngine;
using Infrastructure;

namespace Pokemonomania
{
    public class Pokemon : MonoBehaviour, IImitator<Pokemon>
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private int _id;
        public int Id => _id;

        public void Init(Pokemon other, int id)
        {
            _id = id;
            Imitate(other);
        } 
        
        public void Imitate(Pokemon other)
        {
            _spriteRenderer.sprite = other._spriteRenderer.sprite;
        }
    }
}