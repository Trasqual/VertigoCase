using Cysharp.Threading.Tasks;

namespace UISystem.RouletteGame.ZoneProgressBar
{
    public class ZoneProgressBarController : ZoneProgressBarControllerBase
    {
        protected override void SetupItems()
        {
            for (int i = 0; i < _zoneDatas.Count; i++)
            {
                CreateItem(i);
            }
        }

        public override async UniTask OnProgress(int currentIndex)
        {
            if (currentIndex >= _zoneDatas.Count)
            {
                return;
            }

            await _scrollAnimation.ScrollToObject(currentIndex);
        }
    }
}