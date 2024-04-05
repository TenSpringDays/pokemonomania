using System;
using System.Collections.Generic;
using Pokemonomania.Hud;
using Pokemonomania.Services;
using UnityEngine;
using VContainer.Unity;


namespace Pokemonomania.Flows
{
    public class GameSceneFlow : IStartable, ITickable, IDisposable
    {
        private readonly MainSceneHud _mainSceneHud;
        private readonly IReadOnlyList<IInputService> _inputServices;
        private readonly TimerService _timerService;
        private readonly ComboService _comboService;
        private readonly AppEventsProvider _appEventsProvider;
        private readonly GameService _gameService;

        public GameSceneFlow(MainSceneHud mainSceneHud, 
                             IReadOnlyList<IInputService> inputServices,
                             TimerService timerService,
                             ComboService comboService,
                             AppEventsProvider appEventsProvider,
                             GameService gameService)
        {
            _mainSceneHud = mainSceneHud;
            _inputServices = inputServices;
            _timerService = timerService;
            _comboService = comboService;
            _appEventsProvider = appEventsProvider;
            _gameService = gameService;
        }

        public void Start()
        {
            _appEventsProvider.ApplicationFocus += OnApplicationFocus;
            _mainSceneHud.Enable();
            _gameService.Enable();
        }

        public void Tick()
        {
            float time = Time.time;
            float delta = Time.deltaTime;

            foreach (var inputService in _inputServices)
                inputService.Tick(time);
            _timerService.Tick(delta);
            _comboService.Tick(delta);
            _mainSceneHud.Tick(delta);
            _gameService.Tick(delta);
        }

        public void Dispose()
        {
            _appEventsProvider.ApplicationFocus -= OnApplicationFocus;
        }

        private void OnApplicationFocus(bool obj)
        {
            foreach (var inputService in _inputServices)
                inputService.LostFocus();
        }
    }
}