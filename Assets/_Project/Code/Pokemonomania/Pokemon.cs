using UnityEngine;
using Pokemonomania.Effects;


namespace Pokemonomania
{
    public class Pokemon : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private CatchEffector _effector;
        [SerializeField] private int _id;

        public CatchEffector CatchEffector => _effector;

        public int Id => _id;

        public void Construct(int id)
        {
            _id = id;
        }
    }
}