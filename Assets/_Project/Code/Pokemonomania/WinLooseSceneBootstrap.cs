using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Pokemonomania
{
    public class WinLooseSceneBootstrap : MonoBehaviour
    {
        [SerializeField] private UserStatsView userStatsView;

        private ProjectBootstrap _projectBootstrap;
        private bool _shouldContinue;

        private IEnumerator Start()
        {
            _projectBootstrap = ProjectBootstrap.Find();
            
            userStatsView.Construct(_projectBootstrap.DataService);
            userStatsView.Enable();

            
            for (float t = 0; t < 2f; t += Time.deltaTime)
            {
                yield return null;
                if (Input.anyKeyDown)
                    _shouldContinue = true;
            }
        }

        private void Update()
        {
            if (Input.anyKeyDown)
                _shouldContinue = true;

            if (_shouldContinue)
                SceneManager.LoadScene("1. Home", LoadSceneMode.Single);
        }
    }


}
