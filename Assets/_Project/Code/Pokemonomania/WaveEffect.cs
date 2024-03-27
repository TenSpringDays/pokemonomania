using UnityEngine;


namespace Pokemonomania
{
    public class WaveEffect : MonoBehaviour
    {
        [SerializeField] private PokemonController _controller;
        [SerializeField] private Transform _root;
        [SerializeField] private float _waveLength = 8f;
        [SerializeField] private float _wavePeriod = 6f;
        [SerializeField] private float _waveSpeed = 32f;
        [SerializeField] private Vector3 _scale = new Vector3(1.1f, 0.9f, 1f);
        [SerializeField] private float _fadeDistance = 8f;
        
        private float[] _timeOffsets = new float[16];
        private int _count = 0;

        private void OnEnable()
        {
            _controller.Catched += ControllerOnCatched;
        }

        private void OnDisable()
        {
            _controller.Catched -= ControllerOnCatched;
        }

        private void ControllerOnCatched(Pokemon obj)
        {
            RunWave();
        }

        private void Update()
        {
            UpdateTimers();
            ApplyWave();
        }

        public void RunWave()
        {
            if (_count == _timeOffsets.Length)
                System.Array.Resize(ref _timeOffsets, _count * 2);

            _timeOffsets[_count] = -_wavePeriod;
            _count += 1;
        }


        private void UpdateTimers()
        {
            for (int i = 0; i < _count; i++)
            {
                _timeOffsets[i] += _waveSpeed * Time.deltaTime;

                if (_timeOffsets[i] > _waveLength)
                {
                    _timeOffsets[i] = _timeOffsets[_count - 1];
                    _count -= 1;
                }
            }
        }

        private void ApplyWave()
        {
            for (int i = 0; i < _root.childCount; i++)
            {
                var item = _root.GetChild(i);

                float pos = item.localPosition.y;
                float x = 0f;

                for (int j = 0; j < _count; j++)
                {
                    float t = _timeOffsets[j];
                    float fade = Mathf.Clamp01(-pos / _fadeDistance + 1f);
                    float s = SinSingleWave(pos, t, _wavePeriod) * fade;
                    x = Mathf.Max(x, s);
                }

                item.localScale = Vector3.Lerp(Vector3.one, _scale, x);
            }
        }

        private static float SinSingleWave(float x, float o = 0f, float p = 1f)
        {
            x = (x - o);
            float s0 = Mathf.Sin(x / (p / Mathf.PI));
            float s1 = Mathf.Abs(s0);
            float c0 = -Mathf.Clamp01(x / p - 1f) + 1f;
            float c1 = Mathf.Clamp01(x / p + 1f);
            float c2 = Mathf.Floor(c0) * Mathf.Floor(c1);
            float s2 = s1 * c2;
            return s2;
        }
    }
}