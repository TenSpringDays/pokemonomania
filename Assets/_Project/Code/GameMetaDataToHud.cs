using TMPro;
using UnityEngine;


namespace StoneBreaker
{
    public class GameMetaDataToHud : MonoBehaviour
    {
        [System.Serializable]
        public struct ViewField
        {
            public TMP_Text Text;
            public string Templte;
        }
        
        [SerializeField] private GameMetaData _metaData;
        [SerializeField] private ViewField _maxScore;
        [SerializeField] private ViewField _maxCombo;

        private void OnEnable()
        {
            UpdateHightScore();
            UpdateMaxComob();
        }

        public void UpdateHightScore()
        {
            UpdateView(_maxScore, _metaData.MaxScore);
        }

        public void UpdateMaxComob()
        {
            UpdateView(_maxCombo, _metaData.MaxCombo);
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