using PoolingSystem;
using UnityEngine;

namespace UISystem.Core
{
    public abstract class UIPanel : MonoBehaviour, IPoolable
    {
        [field: SerializeField] public string PoolID { get; private set; }

        public Component Component => this;
        
        public abstract string GetPanelID();
    
        public abstract void Show();
    
        public abstract void Hide();
    
        public virtual void ApplyData(object data) { }
        
        public void OnSpawn()
        {
        }

        public void OnDespawn()
        {
        }
    }
}