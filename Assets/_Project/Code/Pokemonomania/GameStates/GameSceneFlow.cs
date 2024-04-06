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
        private readonly TimerService _timerService;
        private readonly ComboService _comboService;
        private readonly GameController _gameController;

        public GameSceneFlow(MainSceneHud mainSceneHud, 
                             TimerService timerService,
                             ComboService comboService,
                             GameController gameController)
        {
            _mainSceneHud = mainSceneHud;
            _timerService = timerService;
            _comboService = comboService;
            _gameController = gameController;
        }

        public void Start()
        {
            _mainSceneHud.Enable();
            _gameController.Enable();
        }

        public void Tick()
        {
            float delta = Time.deltaTime;
            
            _timerService.Tick(delta);
            _comboService.Tick(delta);
            _mainSceneHud.Tick(delta);
        }

        public void Dispose()
        {
            _gameController.Disable();
        }
    }
}