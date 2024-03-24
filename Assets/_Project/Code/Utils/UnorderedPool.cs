using System;


namespace StoneBreaker.Utils
{
    public class UnorderedPool<T>
    {
        private const int MinSize = 8;
        
        private readonly Func<T> _createFunc;
        private readonly Action<T> _getAction;
        private readonly Action<T> _releaseAction;
        private T[] _pool;
        private int _count;

        public UnorderedPool(Func<T> createFunc, 
                             Action<T> getAction = null,
                             Action<T> releaseAction = null,
                             int initialSize = MinSize)
        {
            _createFunc = createFunc;
            _getAction = getAction;
            _releaseAction = releaseAction;
            
            var size = Math.Max(MinSize, initialSize);
            ResizePool(ref _pool, size, createFunc);
        }

        public int Count => _count;
        public int Capacity => _pool.Length;

        public T Get()
        {
            if (_count >= _pool.Length)
                ResizePool(ref _pool, _pool.Length * 2, _createFunc);

            var item = _pool[_count++];
            _getAction?.Invoke(item);
            return item;
        }

        public T At(int i)
        {
            if (i >= _count)
                throw new Exception("Try get released item.");

            return _pool[i];
        }

        public T ReleaseAt(int i)
        {
            _count--;
            var released = _pool[i];
            _pool[i] = _pool[_count];
            _pool[_count] = released;
            _releaseAction?.Invoke(released);
            return released;
        }

        public void ReleaseAll()
        {
            if (_releaseAction != null)
            {
                for (int i = 0; i < _count; i++)
                    _releaseAction.Invoke(_pool[i]);
            }

            _count = 0;
        }

        private static void ResizePool(ref T[] pool, int size, Func<T> creater)
        {
            int old = pool?.Length ?? 0;
            Array.Resize(ref pool, size);

            for (int i = old; i < pool.Length; i++)
                pool[i] = creater.Invoke();
        }
    }
}