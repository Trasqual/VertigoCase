using System.Collections.Generic;
using UnityEngine;

namespace UISystem.RouletteGame
{
    public class RouletteGameController : MonoBehaviour
    {
        [SerializeField] private List<ZoneData> _zoneDatas = new();

        [SerializeField] private List<RouletteGameElementBase> _rouletteGameElements = new();

        private int _currentZoneIndex;

        public void Awake()
        {
            foreach (RouletteGameElementBase element in _rouletteGameElements)
            {
                element.Initialize(_zoneDatas);
            }
        }

        [ContextMenu("Progress")]
        public void OnProgress()
        {
            _currentZoneIndex++;

            foreach (RouletteGameElementBase element in _rouletteGameElements)
            {
                element.OnProgress(_currentZoneIndex);
            }
        }
    }
}