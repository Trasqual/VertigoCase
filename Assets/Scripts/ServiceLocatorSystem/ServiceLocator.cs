using System;
using System.Collections.Generic;
using UnityEngine;

namespace ServiceLocatorSystem
{
    public class ServiceLocator
    {
        private static ServiceLocator _instance;
        public static ServiceLocator Instance => _instance ??= new ServiceLocator();
        
        private readonly Dictionary<Type, IService> _services;
        
        private ServiceLocator()
        {
            _services = new Dictionary<Type, IService>();
        }
        
        public void RegisterService<T>(T service) where T : IService
        {
            _services[typeof(T)] = service;
        }
        
        public void UnregisterService<T>(T service) where T : IService
        {
            _services.Remove(service.GetType());
        }
        
        public T Get<T>() where T : IService
        {
            Type type = typeof(T);

            _services.TryGetValue(type, out IService service);
            
            if (service == null)
            {
                Debug.LogError($"Service not registered for {type}");
            }

            return (T)service;
        }
    }
}
