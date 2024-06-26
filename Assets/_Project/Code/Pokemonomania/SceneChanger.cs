﻿using UnityEngine;
using UnityEngine.SceneManagement;


namespace Pokemonomania
{
    public class SceneChanger : MonoBehaviour
    {
        [SerializeField] private string _nextScene;

        public void LoadScene()
        {
            LoadScene(_nextScene);
        }

        public void LoadScene(string nextScene)
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}