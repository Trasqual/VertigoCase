using System.Collections.Generic;
using PoolingSystem;
using ServiceLocatorSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace UISystem.RouletteGame.UpcomingZone
{
    public class UpcomingZoneController : RouletteGameElementBase
    {
        [FormerlySerializedAs("_upcomingZoneItemPrefab")][SerializeField] private UpcomingZoneInfoItem _upcomingZoneInfoItemPrefab;

        private List<ZoneData> _zoneDatas = new();

        private List<UpcomingZoneInfoItem> _upcomingZoneItems = new();

        private ObjectPoolManager _poolManager;

        public override void Initialize(List<ZoneData> zoneDatas)
        {
            _zoneDatas = zoneDatas;

            _poolManager = ServiceLocator.Instance.Get<ObjectPoolManager>();
            
            OnProgress(0);
        }

        public override void OnProgress(int currentIndex)
        {
            ClearItems();
            int foundCount = 0;
    
            for (int i = currentIndex + 1; i < _zoneDatas.Count; i++)
            {
                if (_zoneDatas[i].ZoneType is ZoneType.Safe or ZoneType.Super)
                {
                    UpcomingZoneInfoItem upcomingZoneInfoItem = _poolManager.GetObject(_upcomingZoneInfoItemPrefab, parent: transform);
                    upcomingZoneInfoItem.Initialize(_zoneDatas[i], i + 1);
                    _upcomingZoneItems.Add(upcomingZoneInfoItem);
            
                    foundCount++;
                    if (foundCount == 2) break;
                }
            }
        }

        private void ClearItems()
        {
            foreach (var item in _upcomingZoneItems)
            {
                _poolManager.ReleaseObject(item);
            }

            _upcomingZoneItems.Clear();
        }
    }
}