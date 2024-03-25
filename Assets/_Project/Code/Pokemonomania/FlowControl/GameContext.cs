using Infrastructure;
using Pokemonomania.FlowControl;
using UnityEngine;


namespace Pokemonomania
{
    public sealed class GameContext : MonoBehaviour, IGameMachine, IServiceLocator
    {
        private readonly GameMachine _gameMachine = new();
        private readonly ServiceLocator _serviceLocator = new();

        public GameState State => _gameMachine.State;

        [ContextMenu("Play Game")]
        public void Play() => _gameMachine.Play();

        [ContextMenu("Pause Game")]
        public void Pause() => _gameMachine.Pause();

        [ContextMenu("Resume Game")]
        public void Resume() => _gameMachine.Resume();

        [ContextMenu("Finish Game")]
        public void Finish() => _gameMachine.Finish();

        public void AddListener(object listener) => _gameMachine.AddListener(listener);

        public void RemoveListener(object listener) => _gameMachine.RemoveListener(listener);

        public void Add(object service) => _serviceLocator.Add(service);

        public void Remove(object service) => _serviceLocator.Remove(service);

        public T Get<T>() => _serviceLocator.Get<T>();

        public T[] GetAll<T>() => _serviceLocator.GetAll<T>();
    }
}