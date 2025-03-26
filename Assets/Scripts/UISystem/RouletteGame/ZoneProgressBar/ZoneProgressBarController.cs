namespace UISystem.RouletteGame.ZoneProgressBar
{
    public class ZoneProgressBarController : BaseZoneProgressBarController
    {
        protected override void SetupItems()
        {
            for (int i = 0; i < _zoneDatas.Count; i++)
            {
                CreateItem(i);
            }
        }

        public override void OnProgress()
        {
            _currentZoneIndex++;

            if (_currentZoneIndex >= _zoneDatas.Count)
            {
                return;
            }

            _scrollAnimation.ScrollToObject(_currentZoneIndex, 1f);
        }
    }
}