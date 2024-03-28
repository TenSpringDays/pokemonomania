﻿using System;


namespace Pokemonomania.Hud
{
    public interface IInputService
    {
        event Action<int> Pressed;

        void Enable(int maxInputIndexes);

        void Disable();
    }
}