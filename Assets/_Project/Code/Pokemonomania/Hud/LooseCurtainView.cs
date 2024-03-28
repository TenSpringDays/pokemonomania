using UnityEngine;


namespace Pokemonomania.Hud
{
    public class LooseCurtainView : MonoBehaviour
    {
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}