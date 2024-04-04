using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;


namespace Pokemonomania.Services
{
    public class DesktopDataService : IDataService
    {
        private class VirtualSave<T>
        {
            public T Data;
            public string Json;
        }


        private const string SaveFileExtension = ".json";
        private readonly Dictionary<string, object> _virtuals = new();

        public void Save<T>(T obj) where T : new()
        {
            var fileName = typeof(T).ToString() + SaveFileExtension;
            var json = JsonUtility.ToJson(obj, true);
            
            if (_virtuals.TryGetValue(fileName, out object resilt) && resilt is VirtualSave<T> vsave)
            {
                if (json == vsave.Json)
                {
                    Debug.Log("Save without file system");
                    return;
                }
            }
            else
            {
                vsave = new VirtualSave<T>();
                _virtuals.Add(fileName, vsave);
            }
            
            vsave.Data = obj;
            vsave.Json = json;
            
            Debug.Log("Save Use file system");
            var dir = Application.persistentDataPath;
            var file = Path.Combine(dir, fileName);
            File.WriteAllText(file, json, Encoding.UTF8);
        }

        public T Load<T>() where T : new()
        {
            var fileName = typeof(T).ToString() + SaveFileExtension;
            if (_virtuals.TryGetValue(fileName, out object result) && result is VirtualSave<T> vsave)
            {
                Debug.Log("Load without file system");
                return vsave.Data;
            }

            Debug.Log("Load Use file system");
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

            vsave = new VirtualSave<T>();
            vsave.Data = obj;
            vsave.Json = json;
            _virtuals.Add(fileName, vsave);

            return obj;
        }

    }


}