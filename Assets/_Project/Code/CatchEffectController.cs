using StoneBreaker.Utils;
using UnityEngine;


namespace StoneBreaker
{
    public class CatchEffectController : MonoBehaviour
    {
        [SerializeField] private PokemonController _controller;
        [SerializeField] private Transform _catchEffectRoot;
        [SerializeField] private Transform[] _destPoints;
        [SerializeField] private CatchEffectConfig _config;
        [SerializeField] private Pokemon _prefab;

        private UnorderedPool<CatchEffect> _pool2;


        private void OnEnable()
        {
            _controller.Catched += ControllerOnCatched;

            _pool2 = new UnorderedPool<CatchEffect>(
                createFunc: () =>
                {
                    var effect = new CatchEffect(Instantiate(_prefab, _catchEffectRoot));
                    effect.SetPokemonVisibility(false);
                    return effect;
                },
                getAction: x => x.SetPokemonVisibility(true),
                releaseAction: x => x.SetPokemonVisibility(false)
            );
        }

        private void OnDisable()
        {
            _controller.Catched -= ControllerOnCatched;

            _pool2 = null;

            for (int i = 0; i < _catchEffectRoot.childCount; i++)
                Destroy(_catchEffectRoot.GetChild(i).gameObject);
        }

        private void Update()
        {
            for (int i = 0; i < _pool2.Count; i++)
            {
                var effect = _pool2.At(i);
                effect.Update(Time.deltaTime);

                if (effect.InEnd)
                    _pool2.ReleaseAt(i--);
            }
        }

        private void ControllerOnCatched(Pokemon obj)
        {
            _pool2.Get().Init(obj, _destPoints[obj.Id].position, _config);
        }
    }
}