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

        public override void OnProgress(int currentIndex)
        {
            if (currentIndex >= _zoneDatas.Count)
            {
                return;
            }

            CreateItem(currentIndex);

            _scrollAnimation.ScrollToObject(1, () =>
            {
                _poolManager.ReleaseObject(_zoneItems[0]);
                _zoneItems.RemoveAt(0);

                LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)_content);
            });
        }
    }
}