using System.Collections.Generic;
using System.Linq;
using PoolingSystem;
using ServiceLocatorSystem;
using UnityEngine;

namespace UISystem.Core
{
    public class UIManager : IService
    {
        private PanelContainer _panelContainer;
        
        private List<UIPanel> _activePanels;
        
        private ObjectPoolManager _poolManager;
        
        public void Initialize()
        {
            _poolManager = ServiceLocator.Instance.Get<ObjectPoolManager>();
            
            _activePanels = new List<UIPanel>();
            
            _panelContainer = Resources.Load<PanelContainer>("PanelContainer");
        }
        
        public T GetPanel<T>(string panelID) where T : UIPanel
        {
            return (T)_panelContainer.Panels.FirstOrDefault(p => p.GetPanelID() == panelID);
        }
        
        public void OpenPanel(string panelID, object data = null)
        {
            if (_activePanels.Any(p => p.GetPanelID() == panelID))
            {
                Debug.LogWarning($"Panel with id {panelID} is already open!");
                return;
            }

            var prefab = _panelContainer.Panels.FirstOrDefault(p => p.GetPanelID() == panelID);

            var panel = _poolManager.GetObject(prefab);
            panel.ApplyData(data);
            panel.Show();

            _activePanels.Add(panel);
        }
        
        public void ClosePanel(string panelID)
        {
            var panel = _activePanels.FirstOrDefault(p => p.GetPanelID() == panelID);

            if (panel == null)
            {
                Debug.Log($"Panel with id {panelID} is already closed!");
                return;
            }

            panel.Hide();
            _poolManager.ReleaseObject(panel);
            _activePanels.Remove(panel);
        }
    }
}