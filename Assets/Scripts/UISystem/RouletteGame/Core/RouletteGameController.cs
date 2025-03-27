using System.Collections.Generic;
using EventSystem;
using ServiceLocatorSystem;
using UISystem.RouletteGame.Data;
using UISystem.RouletteGame.RewardBar;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.RouletteGame.Core
{
    public class RouletteGameController : MonoBehaviour
    {
        [SerializeField] private Button _exitButton;

        [SerializeField] private List<ZoneData> _zoneDatas = new();

        [SerializeField] private List<RouletteGameElementBase> _rouletteGameElements = new();

        [SerializeField] private TemporaryRewardBarController _temporaryRewardBarController;

        private EventManager _eventManager;

        private int _currentZoneIndex;

        public void Awake()
        {
            _eventManager = ServiceLocator.Instance.Get<EventManager>();

            _eventManager.AddListener<CollectionAnimationFinishedEvent>(OnCollectionAnimationFinished);

            _exitButton.onClick.AddListener(OnExitButtonClicked);

            foreach (RouletteGameElementBase element in _rouletteGameElements)
            {
                element.Initialize(_zoneDatas);
            }
            
            _temporaryRewardBarController.Initialize();
        }

        private void OnCollectionAnimationFinished(object obj)
        {
            OnProgress();
        }

        private void OnExitButtonClicked()
        {
            //TODO SHOW EXIT POPUP
        }

        [ContextMenu("Progress")]
        public void OnProgress()
        {
            _currentZoneIndex++;

            if (_currentZoneIndex >= _zoneDatas.Count)
            {
                //FINISH ROULETTE AND GAIN REWARDS
                return;
            }

            // EventManager eventManager = ServiceLocator.Instance.Get<EventManager>();
            // eventManager.TriggerEvent<RouletteGameProgressedEvent>(new RouletteGameProgressedEvent(_currentZoneIndex));

            foreach (RouletteGameElementBase element in _rouletteGameElements)
            {
                element.OnProgress(_currentZoneIndex);
            }
        }

        private void OnDisable()
        {
            _eventManager.RemoveListener<CollectionAnimationFinishedEvent>(OnCollectionAnimationFinished);
        }
    }
}