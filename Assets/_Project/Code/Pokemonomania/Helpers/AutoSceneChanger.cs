using UnityEngine;
using UnityEngine.SceneManagement;


namespace Pokemonomania.Helpers
{
    public class AutoSceneChanger : MonoBehaviour
    {
        [SerializeField] private string _nextScene;
        [SerializeField] private bool _whenAnyKeyDown = true;
        [SerializeField] private bool _whenDelayElapsed;
        [SerializeField] private float _delay = 2f;

        private float _exitTime = float.MaxValue;
        private bool _anyKeyDownBuffer = false;

        private void Start()
        {
            _exitTime = Time.time + _delay;
        }

        private void Update()
        {
            if (_whenAnyKeyDown)
                _anyKeyDownBuffer |= Input.anyKeyDown;
            
            if (_exitTime < Time.time)
            {
                if (_whenDelayElapsed || _anyKeyDownBuffer)
                    LoadNextScene();
            }
        }

        private void LoadNextScene()
        {
            SceneManager.LoadScene(_nextScene);
        } 
    }
}