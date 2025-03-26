using UnityEngine;
using UnityEngine.UI;

namespace UISystem.RouletteGame.ZoneProgressBar
{
    public class ZoneCurrentProgressBarController : BaseZoneProgressBarController
    {
        protected override void SetupItems()
        {
            CreateItem(0);
        }

        public override void OnProgress()
        {
            _currentZoneIndex++;

            if (_currentZoneIndex >= _zoneDatas.Count)
            {
                return;
            }

            CreateItem(_currentZoneIndex);

            _scrollAnimation.ScrollToObject(1, 1f, () =>
            {
                _poolManager.ReleaseObject(_zoneItems[0]);
                _zoneItems.RemoveAt(0);

                LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)_content);
            });
        }
    }
}