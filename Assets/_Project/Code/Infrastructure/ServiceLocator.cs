using System;
using System.Collections.Generic;
using System.Linq;


namespace Infrastructure
{


    public class ServiceLocator : IServiceLocator
    {
        private readonly List<object> _services = new ();

        public void Add(object service)
        {
            _services.Add(service);
        }

        public void Remove(object service)
        {
            _services.Remove(service);
        }

        public T Get<T>()
        {
            foreach (T service in _services)
            {
                if (service is T result)
                    return result;
            }

            throw new Exception($"Service of type {typeof(T)} not found!");
        }

        public T[] GetAll<T>()
        {
            return _services.OfType<T>().ToArray();
        }
    }


    public interface IUpdatable
    {
        void Update(float delta);
    }


    public interface IStartable
    {
        void Start();
    }
}