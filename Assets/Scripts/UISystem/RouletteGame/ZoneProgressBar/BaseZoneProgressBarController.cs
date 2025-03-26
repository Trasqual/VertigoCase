using System.Collections.Generic;
using PoolingSystem;
using ServiceLocatorSystem;
using UnityEngine;
using Utilities.UI;

namespace UISystem.RouletteGame.ZoneProgressBar
{
    public abstract class BaseZoneProgressBarController : MonoBehaviour
    {
        [SerializeField] protected ZoneProgressItem _zoneItemPrefab;
        [SerializeField] protected ScrollRectAnimation _scrollAnimation;
        [SerializeField] protected Transform _content;

        protected readonly List<ZoneProgressItem> _zoneItems = new();
        protected List<ZoneData> _zoneDatas = new();
        protected int _currentZoneIndex;
        protected ObjectPoolManager _poolManager;

        public void Initialize(List<ZoneData> zoneDatas)
        {
            _zoneDatas = zoneDatas;
            _currentZoneIndex = 0;
            _poolManager = ServiceLocator.Instance.Get<ObjectPoolManager>();
            SetupItems();
        }

        protected abstract void SetupItems();

        protected void CreateItem(int zoneNo)
        {
            ZoneData data = _zoneDatas[zoneNo];
            ZoneProgressItem zoneItem = _poolManager.GetObject(_zoneItemPrefab, parent: _content);
            zoneItem.transform.localScale = Vector3.one;
            zoneItem.Initialize(data.ZoneType, zoneNo + 1);
            _zoneItems.Add(zoneItem);
        }

        public abstract void OnProgress();
    }
}