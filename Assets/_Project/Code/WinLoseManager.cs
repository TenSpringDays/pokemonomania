using UnityEngine;
using UnityEngine.SceneManagement;


namespace StoneBreaker
{

    public class WinLoseManager : MonoBehaviour
    {
        [SerializeField] private GameMetaData _metaData;
        public static WinLoseManager instance;


        public enum WinloseState
        {
            gameStillRunning,
            isGameOver
        }


        public string sceneTargetAfterWinLose;
        WinloseState winloseState;

        void Awake()
        {
            instance = this;
        }

        void Start()
        {
            changeState(WinloseState.gameStillRunning);
        }

        void Update()
        {
            checkState(winloseState);
        }

        public void changeState(WinloseState state)
        {
            winloseState = state;
        }

        void checkState(WinloseState state)
        {
            if (state == WinloseState.isGameOver)
            {
                if (ScoreManager.staticScore > _metaData.MaxScore)
                    _metaData.MaxScore = ScoreManager.staticScore;

                SceneManager.LoadScene(sceneTargetAfterWinLose);
            }
        }
    }
}