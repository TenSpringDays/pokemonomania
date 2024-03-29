using System;
using UnityEngine;


namespace Utils
{
    public class RepeatingClick
    {
        private Action _click;
        private float _downTime;
        private int _callRequests;
        private bool _isDown;

        public RepeatingClick(float delay, Action click)
        {
            Delay = delay;
            _click = click;
        }

        public float Delay { get; set; }

        public void SetListener(Action action)
        {
            _click = action;
        }

        public int Tick(float time)
        {
            int callRequestInTick = 0;
            for (int i = 0; _isDown && time >= _downTime + Delay && i < 32; i++)
            {
                _downTime += Delay;
                callRequestInTick += 1;
            }

            _callRequests += callRequestInTick;

            return callRequestInTick;
        }

        public void Down(float time, bool addRequest)
        {
            if (addRequest)
                _callRequests++;
            
            _downTime = time;
            _isDown = true;
        }

        public void Up()
        {
            _downTime = float.MinValue;
            _isDown = false;
            _callRequests = 0;
        }

        public bool InvokeRequests(bool oneTime)
        {
            int count = oneTime ? Math.Min(1, _callRequests) : _callRequests;

            for (int i = 0; i < count; i++)
                _click?.Invoke();

            _callRequests = 0;
            return count > 0;
        }
    }
}