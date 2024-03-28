using System;
using System.IO;
using System.Text;
using Pokemonomania.Data;
using UnityEngine;


namespace Pokemonomania.Services
{
    public class DesktopDataService : IDataService
    {
        private const string UserStatsFile = "UserStats.json";

        public UserStats LoadUserStats()
        {
            var dir = Application.persistentDataPath;
            var file = Path.Combine(dir, UserStatsFile);
            if (!File.Exists(file))
                return null;
            
            var jsno = File.ReadAllText(file, Encoding.UTF8);
            return JsonUtility.FromJson<UserStats>(jsno);
        }

        public void SaveUserStats(UserStats stats)
        {
            var dir = Application.persistentDataPath;
            var file = Path.Combine(dir, UserStatsFile);
            var json = JsonUtility.ToJson(stats, true);
            File.WriteAllText(file, json, Encoding.UTF8);
        }
    }


}