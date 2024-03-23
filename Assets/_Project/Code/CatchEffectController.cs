using System.Collections.Generic;
using UnityEngine;


namespace StoneBreaker
{
    public class CatchEffectController : MonoBehaviour
    {
        private const int InitialPoolSize = 8;
        
        [SerializeField] private PokemonController _controller;
        [SerializeField] private Transform[] _destPoints;
        [SerializeField] private Vector2 _impulseAngleSpread = new Vector2(-45f, 45f);
        [SerializeField] private Vector2 _impulseSpeedSpread = new Vector2(10f, 30f);
        [SerializeField] private float _disableDistanceTreshold = 0.5f;

        private CatchEffect[] _pool = new CatchEffect[InitialPoolSize];
        private int _spawned;


        private void OnEnable()
        {
            _controller.Catched += ControllerOnCatched;
        }

        private void OnDisable()
        {
            _controller.Catched -= ControllerOnCatched;
        }

        private void Update()
        {
            for (int i = 0; i < _spawned; i++)
            {
                var effect = _pool[i];
                effect.Update(Time.deltaTime);
                
                if (effect.DistToEnd < _disableDistanceTreshold)
                {
                    _spawned--;
                    effect.Pokemon.gameObject.SetActive(false);
                    _pool[i] = _pool[_spawned];
                    _pool[_spawned] = effect;
                    i--;
                }
            }
        }

        private void ControllerOnCatched(Pokemon obj)
        {
            if (_spawned >= _pool.Length)
                System.Array.Resize(ref _pool, _pool.Length * 2);
            
            
            ref CatchEffect effect = ref _pool[_spawned];
            _spawned += 1;

            if (effect == null)
            {
                effect = new CatchEffect();
                effect.Pokemon = Instantiate(obj);
            }

            Vector3 dest = _destPoints[obj.Id].position;
            Pokemon pok = effect.Pokemon;
            Transform pokTrans = pok.transform;
            Transform objTrans = obj.transform;
            Vector3 dir = dest - pokTrans.position;
            float dist = dir.magnitude;
            Vector3 ndir = dir / dist;
            float angle = Random.Range(_impulseAngleSpread.x, _impulseAngleSpread.y);
            float moveSpeed = Random.Range(_impulseSpeedSpread.x, _impulseSpeedSpread.y);
            
            pok.Imitate(obj);
            pok.gameObject.SetActive(true);
            pokTrans.SetPositionAndRotation(objTrans.position, objTrans.rotation);
            effect.Destination = dest;
            effect.Impulse = Quaternion.AngleAxis(angle, Vector3.forward) * ndir * moveSpeed;
            effect.Speed = moveSpeed;
            effect.DistToEnd = dist;
        }
    }
}