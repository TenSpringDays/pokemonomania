using UnityEngine;


namespace Pokemonomania.Services
{
    public class TimerService
    {
        private float _elapsed;

        public float Elapsed => _elapsed;

        public bool Enabled { get; set; }

        public void Tick(float delta)
        {
            if (Enabled)
                _elapsed += delta;
        }
    }
}