using StoneBreaker.Infrastructure;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace StoneBreaker
{
    public class WinLoseManager : Singleton<WinLoseManager>
    {
        public enum WinloseState
        {
            GameStillRunning,
            IsGameOver
        }


        [SerializeField] private ScoreService _scoreService;
        [SerializeField] private GameMetaData _metaData;
        [SerializeField] private string sceneTargetAfterWinLose;
        
        private WinloseState winloseState;
        

        private void Start()
        {
            ChangeState(WinloseState.GameStillRunning);
        }

        private void Update()
        {
            checkState(winloseState);
        }

        public void ChangeState(WinloseState state)
        {
            winloseState = state;
        }

        private void checkState(WinloseState state)
        {
            if (state == WinloseState.IsGameOver)
            {

                if (ScoreService.Instance.Score > _metaData.MaxScore)
                    _metaData.MaxScore = ScoreService.Instance.Score;
                
                SceneManager.LoadScene(sceneTargetAfterWinLose);
            }
        }
    }
}