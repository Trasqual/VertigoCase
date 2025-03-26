using System;
using System.Collections.Generic;
using ServiceLocatorSystem;
using UnityEngine;

namespace PoolingSystem
{
    public class ObjectPoolManager : IService
    {
        private readonly Dictionary<string, Queue<IPoolable>> _pools = new();

        private Transform _poolContainer;

        public ObjectPoolManager()
        {
            Initialize();
        }

        public void Initialize()
        {
            if (_poolContainer == null)
            {
                _poolContainer = new GameObject("Pool Container").transform;
            }
        }
    
        public T GetObject<T>(T poolable, Vector3 position = default, Quaternion rotation = default, Transform parent = null) where T : IPoolable
        {
            Queue<IPoolable> pool = GetPool(poolable) ?? CreatePool(poolable);

            if (pool.Count <= 0)
            {
                Component spawn = UnityEngine.Object.Instantiate(poolable.Component, _poolContainer, true);
                spawn.gameObject.SetActive(false);
                spawn.transform.localPosition = Vector3.zero;

                pool.Enqueue((IPoolable)spawn);
            }

            IPoolable pooledObject = pool.Dequeue();

            if (pooledObject != null)
            {
                pooledObject.Component.transform.SetPositionAndRotation(position, rotation);
                pooledObject.Component.transform.SetParent(parent == null ? _poolContainer : parent);
                pooledObject.Component.gameObject.SetActive(true);
                pooledObject.OnSpawn();
                return (T)pooledObject;
            }

            throw new Exception("Pooled item doesn't contain the requested component type!");
        }

        public void ReleaseObject<T>(T poolable) where T : IPoolable
        {
            if (_poolContainer == null)
            {
                throw new Exception("Pool container is not initialized.");
            }

            if (_pools.TryGetValue(poolable.PoolID, out Queue<IPoolable> pool))
            {
                poolable.OnDespawn();
                poolable.Component.gameObject.SetActive(false);
                poolable.Component.transform.SetParent(_poolContainer);
                pool.Enqueue(poolable);
            }
            else
            {
                UnityEngine.Object.Destroy(poolable.Component.gameObject);
            }
        }

        private Queue<IPoolable> GetPool(IPoolable poolable)
        {
            _pools.TryGetValue(poolable.PoolID, out Queue<IPoolable> pool);
            return pool;
        }

        private Queue<IPoolable> CreatePool<T>(T poolable) where T : IPoolable
        {
            Queue<IPoolable> pool = new ();

            _pools.Add(poolable.PoolID, pool);

            return pool;
        }

        public void PrePopulatePool<T>(T poolable, int count) where T : IPoolable
        {
            for (int i = 0; i < count; i++)
            {
                Component spawn = UnityEngine.Object.Instantiate(poolable.Component);
                ReleaseObject((IPoolable)spawn);
            }
        }

        public void Clear()
        {
            foreach (Queue<IPoolable> pool in _pools.Values)
            {
                while (pool.Count > 0)
                {
                    var item = pool.Dequeue();
                    if (item == null || item.Component == null)
                    {
                        continue;
                    }
                    UnityEngine.Object.Destroy(item.Component.gameObject);
                }
                pool.Clear();
            }

            _pools.Clear();
        }
    }
}