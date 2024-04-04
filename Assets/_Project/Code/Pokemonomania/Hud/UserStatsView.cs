using System;
using Pokemonomania.Data;
using Pokemonomania.Services;
using TMPro;
using UnityEngine;


namespace Pokemonomania
{
    public class UserStatsView : MonoBehaviour
    {
        [System.Serializable]
        public struct ViewField
        {
            public TMP_Text Text;
            public string Templte;
        }
        
        public enum StatsType
        {
            Best,
            Last,
        }


        [SerializeField] private StatsType _statsType;
        [SerializeField] private ViewField _maxScore;
        [SerializeField] private ViewField _maxCombo;
        [SerializeField] private ViewField _longestRun;

        private IDataService _dataService;

        public void Construct(IDataService dataService)
        {
            _dataService = dataService;
        }

        public void Enable()
        {
            var stats = _dataService.Load<UserStats>();
            var values = _statsType switch
            {
                StatsType.Best => stats.Best,
                StatsType.Last => stats.Last,
                _ => throw new ArgumentOutOfRangeException()
            };
            
            UpdateView(_longestRun, values.Elapsed);
            UpdateView(_maxScore, values.Score);
            UpdateView(_maxCombo, values.Combo);
        }

        private static void UpdateView(ViewField field, float data)
        {
            if (!field.Text)
                return;
            var templte = string.IsNullOrEmpty(field.Templte) ? "{}" : field.Templte;
            field.Text.SetText(templte, data);
        }
    }
}