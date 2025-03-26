using UnityEngine;

namespace PoolingSystem
{
    public interface IPoolable
    {
        public string PoolID { get; }
        public Component Component { get; }
        public void OnSpawn();
        public void OnDespawn();
    }
}