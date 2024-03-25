using UnityEngine;


namespace Infrastructure
{
    public class ServiceLocatorInstaller : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour[] _monoBehavioursServices;
        [SerializeField] private ScriptableObject[] _scriptableObjectsServices;
        
        private ServiceLocator _locator;

        private void Awake()
        {
            _locator = new ServiceLocator();

            foreach (var service in _monoBehavioursServices)
                _locator.Add(service);

            foreach (var service in _scriptableObjectsServices)
                _locator.Add(service);
        }
    }


}