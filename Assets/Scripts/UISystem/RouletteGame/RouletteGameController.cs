using System.Collections.Generic;
using UISystem.RouletteGame.ZoneProgressBar;
using UnityEngine;

namespace UISystem.RouletteGame
{
    public class RouletteGameController : MonoBehaviour
    {
        [SerializeField] private List<ZoneData> _zoneDatas = new();

        [SerializeField] private BaseZoneProgressBarController _zoneProgressBarController;
        [SerializeField] private BaseZoneProgressBarController _zoneCurrentBarController;

        private int _currentZoneIndex;

        public void Awake()
        {
            if (_zoneProgressBarController != null)
            {
                _zoneProgressBarController.Initialize(_zoneDatas);
                _zoneCurrentBarController.Initialize(_zoneDatas);
            }
        }

        [ContextMenu("Progress")]
        public void OnProgress()
        {
            _zoneProgressBarController.OnProgress();
            _zoneCurrentBarController.OnProgress();
        }
    }
}