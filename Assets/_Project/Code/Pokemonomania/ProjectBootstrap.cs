using System;
using Pokemonomania.Services;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Pokemonomania
{
    public class ProjectBootstrap : MonoBehaviour
    {
        public IDataService DataService { get; private set; }

        private void Start()
        {
            DataService = new DesktopDataService();
            
            DontDestroyOnLoad(this);
            SceneManager.LoadScene("1. Home", LoadSceneMode.Single);
        }

        public static ProjectBootstrap Find()
        {
            return GameObject.FindGameObjectWithTag("ProjectBootstrap").GetComponent<ProjectBootstrap>();
        }
    }
}