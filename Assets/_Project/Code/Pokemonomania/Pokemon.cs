using UnityEngine;
using Infrastructure;
using Pokemonomania.Effects;


namespace Pokemonomania
{
    public class Pokemon : MonoBehaviour, IImitator<Pokemon>
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private CatchEffector _effector;

        private int _id;
        public int Id => _id;

        public CatchEffector Effector => _effector;

        public CatchEffector CatchEffector => _effector;

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