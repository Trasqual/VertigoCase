using System.Collections.Generic;
using PoolingSystem;
using ServiceLocatorSystem;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Utilities.UI;

namespace UISystem.RouletteGame.ZoneProgressBar
{
    public class ZoneProgressBarController : MonoBehaviour
    {
        [SerializeField] private ZoneProgressItem _zoneProgressItemPrefab;
        [SerializeField] private ZoneProgressItem _zoneCurrentItemPrefab;

        [SerializeField] private ScrollRectAnimation _progressAnimation;
        [SerializeField] private ScrollRectAnimation _currentAnimation;

        [SerializeField] private Transform _progressContent;
        [SerializeField] private Transform _currentContent;

        private readonly List<ZoneProgressItem> _progressItems = new();
        private readonly List<ZoneProgressItem> _currentItems = new();

        private List<ZoneData> _zoneDatas = new();

        private int _currentZoneIndex;

        private ObjectPoolManager _poolManager;

        public void Initialize(List<ZoneData> zoneDatas)
        {
            _zoneDatas = zoneDatas;

            _currentZoneIndex = 0;

            _poolManager = ServiceLocator.Instance.Get<ObjectPoolManager>();

            for (int i = 0; i < zoneDatas.Count; i++)
            {
                CreateItem(_zoneProgressItemPrefab, _progressContent, i, _progressItems);
            }

            CreateItem(_zoneCurrentItemPrefab, _currentContent, 0, _currentItems);
        }

        private void CreateItem(ZoneProgressItem prefab, Transform parent, int zoneNo, List<ZoneProgressItem> list)
        {
            ZoneData data = _zoneDatas[zoneNo];
            ZoneProgressItem zoneItem = _poolManager.GetObject(prefab, parent: parent);
            zoneItem.Initialize(data.ZoneType, zoneNo + 1);
            list.Add(zoneItem);
        }

        public void OnProgress()
        {
            _currentZoneIndex++;

            if (_currentZoneIndex >= _zoneDatas.Count)
            {
                return;
            }

            _progressAnimation.ScrollToObject(_currentZoneIndex, 1f);


            CreateItem(_zoneCurrentItemPrefab, _currentContent, _currentZoneIndex, _currentItems);

            _currentAnimation.ScrollToObject(1, 1f, () =>
            {
                _poolManager.ReleaseObject(_currentItems[0]);
                _currentItems.RemoveAt(0);
                LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)_currentContent);
            });
        }
    }
}