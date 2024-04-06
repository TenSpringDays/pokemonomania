using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;


namespace Pokemonomania.Hud
{
    public class ApplyTween : MonoBehaviour
    {
        [SerializeField] private float _duration = 0.5f;
        [SerializeField] private bool _lockAfterFirstExecute = true;
        [FormerlySerializedAs("_grapic")] [SerializeField] private Graphic _grappic; 
        [SerializeField] private Vector3 _endScale = new Vector3(1.3f, 1.3f, 1f);
        [SerializeField] private UnityEvent _onExecuted;
        [SerializeField] private UnityEvent _onEnded;

        private bool _executed = false;
        
        public void Execute()
        {
            if (_lockAfterFirstExecute && _executed)
                return;

            StopAllCoroutines();
            StartCoroutine(TweenRoutine());
        }

        private IEnumerator TweenRoutine()
        {
            _executed = true;
            _onExecuted?.Invoke();

            for (float time = 0; time < _duration; time += Time.unscaledDeltaTime)
            {
                yield return null;
                
                float t = Easing.InOutCubic(time / _duration);
                _grappic.transform.localScale = Vector3.Lerp(_grappic.transform.localScale, _endScale, t);
                var col = _grappic.color;
                col.a = Mathf.Lerp(col.a, 0f, t);
                _grappic.color = col;
            }
            
            _onEnded?.Invoke();
            _executed = false;
        }
    }
}