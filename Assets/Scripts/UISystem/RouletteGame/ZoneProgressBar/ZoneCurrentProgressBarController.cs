using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.RouletteGame.ZoneProgressBar
{
    public class ZoneCurrentProgressBarController : ZoneProgressBarControllerBase
    {
        protected override void SetupItems()
        {
            CreateItem(0);
        }

        public override async UniTask OnProgress(int currentIndex)
        {
            if (currentIndex >= _zoneDatas.Count)
            {
                return;
            }

            CreateItem(currentIndex);

            await _scrollAnimation.ScrollToObject(1, () =>
            {
                _poolManager.ReleaseObject(_zoneItems[0]);
                _zoneItems.RemoveAt(0);

                LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)_content);
            });
        }
    }
}