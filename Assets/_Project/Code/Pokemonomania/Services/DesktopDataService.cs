using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;


namespace Pokemonomania.Services
{
    public class DesktopDataService : IDataService, IDisposable
    {
        private readonly AppEventsProvider _appEventsProvider;


        private class VirtualSave
        {
            public object Data;
            public string Json;
        }


        private const string SaveFileExtension = ".json";
        private readonly Dictionary<string, VirtualSave> _virtuals = new();

        public DesktopDataService(AppEventsProvider appEventsProvider)
        {
            _appEventsProvider = appEventsProvider;
            _appEventsProvider.ApplicationQuit += OnApplicationQuit;
        }

        public void Dispose()
        {
            _appEventsProvider.ApplicationQuit -= OnApplicationQuit;
        }

        private void OnApplicationQuit()
        {
            foreach ((string fileName, VirtualSave vsave) in _virtuals)
            {
                Debug.Log($"Save '{fileName}' Use file system");
                
                var dir = Application.persistentDataPath;
                var file = Path.Combine(dir, fileName);
                File.WriteAllText(file, vsave.Json, Encoding.UTF8);
            }
        }

        public void Save<T>(T obj) where T : new()
        {
            var fileName = typeof(T).ToString() + SaveFileExtension;
            
            if (_virtuals.TryGetValue(fileName, out VirtualSave vsave) && vsave.Data is T)
            {
                Debug.Log($"Save '{fileName}' without file system");
            }
            else
            {
                vsave = new VirtualSave();
                _virtuals.Add(fileName, vsave);
            }
            
            vsave.Data = obj;
            vsave.Json = JsonUtility.ToJson(obj, true);
        }

        public T Load<T>() where T : new()
        {
            var fileName = typeof(T).ToString() + SaveFileExtension;
            if (_virtuals.TryGetValue(fileName, out VirtualSave vsave) && vsave.Data is T)
            {
                Debug.Log($"Load '{fileName}' without file system");
                return (T)vsave.Data;
            }

            Debug.Log($"Load '{fileName}' Use file system");
            var dir = Application.persistentDataPath;
            var file = Path.Combine(dir, fileName);

            T obj;
            string json;
            
            if (!File.Exists(file))
            {
                obj = new T();
                json = JsonUtility.ToJson(obj, true);
            }
            else
            {
                json = File.ReadAllText(file, Encoding.UTF8);
                obj = JsonUtility.FromJson<T>(json);
            }

            vsave = new VirtualSave();
            vsave.Data = obj;
            vsave.Json = json;
            _virtuals.Add(fileName, vsave);

            return obj;
        }

    }


}