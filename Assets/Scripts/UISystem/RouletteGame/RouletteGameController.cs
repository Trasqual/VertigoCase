using System.Collections.Generic;
using UISystem.RouletteGame.ZoneProgressBar;
using UnityEngine;

namespace UISystem.RouletteGame
{
    public class RouletteGameController : MonoBehaviour
    {
        [SerializeField] private List<ZoneData> _zoneDatas = new();

        [SerializeField] private ZoneProgressBarController _zoneProgressBarController;

        private int _currentZoneIndex;

        public void Awake()
        {
            if (_zoneProgressBarController != null)
            {
                _zoneProgressBarController.Initialize(_zoneDatas);
            }
        }
    }
}