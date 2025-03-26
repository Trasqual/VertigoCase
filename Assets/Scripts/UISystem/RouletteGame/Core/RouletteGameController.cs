using System.Collections.Generic;
using UISystem.RouletteGame.Data;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.RouletteGame
{
    public class RouletteGameController : MonoBehaviour
    {
        [SerializeField] private Button _exitButton;
        
        [SerializeField] private List<ZoneData> _zoneDatas = new();

        [SerializeField] private List<RouletteGameElementBase> _rouletteGameElements = new();

        private int _currentZoneIndex;

        public void Awake()
        {
            _exitButton.onClick.AddListener(OnExitButtonClicked);
            
            foreach (RouletteGameElementBase element in _rouletteGameElements)
            {
                element.Initialize(_zoneDatas);
            }
        }

        private void OnExitButtonClicked()
        {
            
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