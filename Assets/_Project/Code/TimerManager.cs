using StoneBreaker.Infrastructure;
using UnityEngine;
using UnityEngine.UI;


namespace StoneBreaker
{
    public class TimerManager : Singleton<TimerManager>
    {
        [SerializeField] private int maxTime;
        [SerializeField] private int decreasingSpeed = 1;
        [SerializeField] private float time;

        [SerializeField] private GameObject timerFillObject;

        [SerializeField] private GameObject timerText;
        [SerializeField] private string additionalText;

        private void Start()
        {
            time = maxTime;
            UpdateTimerText();
        }

        private void Update()
        {
            time -= (decreasingSpeed * Time.deltaTime);
            UpdateTimerText();
            UpdateTimerFillObject();

            if (time <= 0f)
            {
                WinLoseManager.Instance.ChangeState(WinLoseManager.WinloseState.IsGameOver);
            }
        }

        private void UpdateTimerText()
        {
            timerText.GetComponent<Text>().text = additionalText + time.ToString("0.00");
        }

        private void UpdateTimerFillObject()
        {
            timerFillObject.GetComponent<Image>().fillAmount = time / maxTime;
        }
    }
}