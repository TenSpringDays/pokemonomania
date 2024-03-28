using UnityEngine;


namespace Pokemonomania.Services
{
    public class TimerService
    {
        private float _elapsed;

        public float Elapsed => _elapsed;

        public void Tick(float delta)
        {
            _elapsed += delta;
        }
    }
}