using System.Collections.Generic;
using UISystem.RouletteGame.UpcomingZone;
using UISystem.RouletteGame.ZoneProgressBar;
using UnityEngine;

namespace UISystem.RouletteGame
{
    public class RouletteGameController : MonoBehaviour
    {
        [SerializeField] private List<ZoneData> _zoneDatas = new();

        [SerializeField] private BaseZoneProgressBarController _zoneProgressBarController;
        [SerializeField] private BaseZoneProgressBarController _zoneCurrentBarController;
        [SerializeField] private UpcomingZoneController _upcomingZoneController;

        private int _currentZoneIndex;

        public void Awake()
        {
            if (_zoneProgressBarController != null)
            {
                _zoneProgressBarController.Initialize(_zoneDatas);
            }

            if (_zoneCurrentBarController != null)
            {
                _zoneCurrentBarController.Initialize(_zoneDatas);
            }

            if (_upcomingZoneController != null)
            {
                _upcomingZoneController.Initialize(_zoneDatas);
            }
        }

        [ContextMenu("Progress")]
        public void OnProgress()
        {
            _currentZoneIndex++;

            _zoneProgressBarController.OnProgress(_currentZoneIndex);
            _zoneCurrentBarController.OnProgress(_currentZoneIndex);
            _upcomingZoneController.OnProgress(_currentZoneIndex);
        }
    }
}