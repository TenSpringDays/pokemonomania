using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;


namespace StoneBreaker
{
    public class BlinkingEffect : MonoBehaviour
    {
        [SerializeField] private MaskableGraphic _target;
        [SerializeField] private float _pingpongLen = 1f;

        private void Update()
        {
            TextBlinking();
        }

        private void TextBlinking()
        {
            float a = Mathf.PingPong(Time.unscaledTime, _pingpongLen);
            a = Easing.OutSine(a);
            SetAlpha(_target, a);
        }

        private static void SetAlpha(Graphic target, float alpha)
        {
            var col = target.color;
            col.a = alpha;
            target.color = col;
        }
    }
}