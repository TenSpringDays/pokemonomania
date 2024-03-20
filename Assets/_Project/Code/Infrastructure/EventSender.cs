using System;
using UnityEngine;


namespace StoneBreaker.Infrastructure
{
    public abstract class EventSender<T> : ScriptableObject
    {
        [SerializeField] private bool _checkRecursive = true;
        [SerializeField] private bool _warningAfterFirstRecursiveCall = true;
        [SerializeField] private int _maxRecursiveRaises = 64;

        private IEventReceiver<T>[] _translators;
        private int _count;
        private int _raiseCount = 0;

        private void OnEnable()
        {
            _translators = new IEventReceiver<T>[8];
            _count = 0;
            _raiseCount = 0;
        }

        public void Raise(object sender, T argument)
        {
            TryHandleErrors(sender, argument);

            _raiseCount += 1;

            for (int i = 0; i < _count; i++)
                _translators[i].Receive(sender, argument);

            _raiseCount -= 1;
        }

        private void TryHandleErrors(object sender, T argument)
        {
            if (_checkRecursive)
            {
                if (_raiseCount > _maxRecursiveRaises)
                {
                    throw new UnityException($"Recursive count raises in {nameof(EventSender<T>)}:{name} " +
                                             $"overflow 'MaxRecursiveRaises': {_maxRecursiveRaises}");
                }

                if (_warningAfterFirstRecursiveCall && _raiseCount > 0)
                {
                    Debug.unityLogger.LogWarning(
                        "RAISE",
                        $"Recursive rise: {_raiseCount} time; current sender: {sender}, argument: {argument}",
                        this);
                }
            }
        }

        public void Subscribe(IEventReceiver<T> translator)
        {
            if (_count + 1 >= _translators.Length)
                Array.Resize(ref _translators, _translators.Length * 2);

            _translators[_count] = translator;
            _count++;
        }

        public void Unsubscribe(IEventReceiver<T> translator)
        {
            int toDelete = Array.FindIndex(_translators, 0, _count, x => ReferenceEquals(x, translator));

            if (toDelete >= 0)
            {
                var last = _count - 1;
                _translators[toDelete] = _translators[last];
                _translators[last] = null;
                _count--;
            }
        }
    }


}