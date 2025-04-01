using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using PoolingSystem;
using ServiceLocatorSystem;
using UISystem.RouletteGame.Data;
using UnityEngine;
using UnityEngine.Serialization;

namespace UISystem.RouletteGame.UpcomingZoneBar
{
    public class UpcomingZoneBarController : RouletteGameElementBase
    {
        [FormerlySerializedAs("_upcomingZoneInfoItemPrefab")][SerializeField] private UpcomingZoneItem _upcomingZoneItemPrefab;

        private List<ZoneData> _zoneDatas = new();

        private readonly List<UpcomingZoneItem> _upcomingZoneItems = new();

        private ObjectPoolManager _poolManager;

        public override void Initialize(List<ZoneData> zoneDatas)
        {
            _zoneDatas = zoneDatas;

            _poolManager = ServiceLocator.Instance.Get<ObjectPoolManager>();

            OnProgress(0).Forget();
        }

        public override async UniTask OnProgress(int currentIndex)
        {
            Clear();
            int foundCount = 0;

            for (int i = currentIndex + 1; i < _zoneDatas.Count; i++)
            {
                if (_zoneDatas[i].ZoneType is ZoneType.Safe or ZoneType.Super)
                {
                    UpcomingZoneItem upcomingZoneItem = _poolManager.GetObject(_upcomingZoneItemPrefab, parent: transform);
                    upcomingZoneItem.Initialize(_zoneDatas[i], i + 1);
                    upcomingZoneItem.transform.localScale = Vector3.one;
                    _upcomingZoneItems.Add(upcomingZoneItem);

                    foundCount++;
                    if (foundCount == 2) break;
                }
            }

            await UniTask.CompletedTask;
        }

        public override void Clear()
        {
            foreach (var item in _upcomingZoneItems)
            {
                _poolManager.ReleaseObject(item);
            }

            _upcomingZoneItems.Clear();
        }
    }
}