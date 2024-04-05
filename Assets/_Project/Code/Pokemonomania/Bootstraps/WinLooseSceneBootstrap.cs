using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;


namespace Pokemonomania.Bootstrap
{
    public class WinLooseSceneBootstrap : LifetimeScope
    {
        private bool _shouldContinue;

        protected override void Configure(IContainerBuilder builder)
        {
        }

        private IEnumerator Start()
        {
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
