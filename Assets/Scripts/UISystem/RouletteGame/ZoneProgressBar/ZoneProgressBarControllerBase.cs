using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using PoolingSystem;
using ServiceLocatorSystem;
using UISystem.RouletteGame.Data;
using UnityEngine;
using Utilities.UI;

namespace UISystem.RouletteGame.ZoneProgressBar
{
    public abstract class ZoneProgressBarControllerBase : RouletteGameElementBase
    {
        [SerializeField] protected ZoneProgressItem _zoneItemPrefab;
        [SerializeField] protected ScrollRectAnimation _scrollAnimation;
        [SerializeField] protected Transform _content;

        protected readonly List<ZoneProgressItem> _zoneItems = new();
        protected List<ZoneData> _zoneDatas = new();
        protected ObjectPoolManager _poolManager;

        public override void Initialize(List<ZoneData> zoneDatas)
        {
            _zoneDatas = zoneDatas;
            _poolManager = ServiceLocator.Instance.Get<ObjectPoolManager>();
            SetupItems();
        }

        public void ApplySettings(ScrollAnimationSettings scrollAnimationSettings)
        {
            _scrollAnimation.ApplySettings(scrollAnimationSettings);
        }

        protected abstract void SetupItems();

        protected void CreateItem(int zoneNo)
        {
            ZoneData data = _zoneDatas[zoneNo];
            ZoneProgressItem zoneItem = _poolManager.GetObject(_zoneItemPrefab, parent: _content);
            zoneItem.transform.localScale = Vector3.one;
            zoneItem.Initialize(data, zoneNo + 1);
            _zoneItems.Add(zoneItem);
        }

        public abstract override UniTask OnProgress(int currentIndex);

        public override void Clear()
        {
            foreach (ZoneProgressItem item in _zoneItems)
            {
                _poolManager.ReleaseObject(item);
            }

            _zoneItems.Clear();

            _content.localPosition = Vector3.zero;
        }
    }
}