using UnityEngine;
using Pokemonomania.Effects;


namespace Pokemonomania
{
    public class Pokemon : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private CatchEffector _effector;

        public CatchEffector CatchEffector => _effector;
    }
}