using System.Collections.Generic;
using UnityEngine;


namespace Pokemonomania.FlowControl
{
    public class GameMachine : IGameMachine
    {
        private readonly List<IStartGameListener> _startListeners = new();
        private readonly List<IPauseGameListener> _pauseListeners = new();
        private readonly List<IResumeGameListener> _resumeListeners = new();
        private readonly List<IFinishGameListener> _finishListeners = new();
        private GameState _state = GameState.Off;
        
        public GameState State => _state;

        public void Play()
        {
            if (_state != GameState.Off)
                throw new UnityException($"Start game can only from '{GameState.Off}' state!");

            _state = GameState.Play;

            for (int i = 0; i < _startListeners.Count; i++)
                _startListeners[i].OnStartGame();
        }
        
        public void Pause()
        {
            if (_state != GameState.Play)
                throw new UnityException($"Pause game can only from '{GameState.Play}' state!");

            _state = GameState.Pause;
            
            for (int i = 0; i < _pauseListeners.Count; i++)
                _pauseListeners[i].OnPauseGame();
        }

        public void Resume()
        {
            if (_state != GameState.Pause)
                throw new UnityException($"Resume game can only from '{GameState.Pause}' state!");

            _state = GameState.Play;
            
            for (int i = 0; i < _resumeListeners.Count; i++)
                _resumeListeners[i].OnResumeGame();
        }

        public void Finish()
        {
            if (_state != GameState.Finish)
                throw new UnityException($"Finish game can only from '{GameState.Play}' state!");

            _state = GameState.Finish;
            
            for (int i = 0; i < _finishListeners.Count; i++)
                _finishListeners[i].OnFinishGame();
        }

        public void AddListener(object listener)
        {
            if (listener is IStartGameListener startGameListener)
                _startListeners.Add(startGameListener);
            
            if (listener is IPauseGameListener pauseGameListener)
                _pauseListeners.Add(pauseGameListener);
            
            if (listener is IResumeGameListener resumeGameListener)
                _resumeListeners.Add(resumeGameListener);
            
            if (listener is IFinishGameListener finishGameListener)
                _finishListeners.Add(finishGameListener);
        }

        public void RemoveListener(object listener)
        {
            if (listener is IStartGameListener startGameListener)
                _startListeners.Remove(startGameListener);
            
            if (listener is IPauseGameListener pauseGameListener)
                _pauseListeners.Remove(pauseGameListener);
            
            if (listener is IResumeGameListener resumeGameListener)
                _resumeListeners.Remove(resumeGameListener);
            
            if (listener is IFinishGameListener finishGameListener)
                _finishListeners.Remove(finishGameListener);
        }
    }
}