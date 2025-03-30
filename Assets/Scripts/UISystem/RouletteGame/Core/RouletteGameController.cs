using System.Collections.Generic;
using EventSystem;
using ServiceLocatorSystem;
using UISystem.Core;
using UISystem.Popups;
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

        private UIManager _uiManager;

        private int _currentZoneIndex;

        public void Awake()
        {
            _eventManager = ServiceLocator.Instance.Get<EventManager>();

            _uiManager = ServiceLocator.Instance.Get<UIManager>();

            SubscribeToEvents();

            InitializeElements();

            _exitButton.onClick.AddListener(OnExitButtonClicked);
        }

        private void SubscribeToEvents()
        {
            _eventManager.AddListener<CollectionAnimationFinishedEvent>(OnCollectionAnimationFinished);
        }

        private void InitializeElements()
        {
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
            //TODO : return if wrong state

            AreYouSurePopupData areYouSurePopupData = new("Are you sure you want to exit the roulette game?",
                                                          onConfirm: () =>
                                                          {
                                                              _temporaryRewardBarController.ClaimRewardsPermanently();
                                                              _uiManager.ClosePanel(UIIDs.AreYouSurePopup);
                                                          },
                                                          onCancel: () =>
                                                          {
                                                              _uiManager.ClosePanel(UIIDs.AreYouSurePopup);
                                                          });
            ShowAreYouSurePopup(areYouSurePopupData);
        }

        private void OnBombClaimed()
        {
            RevivePopupData revivePopupData = new("Oh no! The bomb is about to explode!",
                                                  onRevive: () =>
                                                  {
                                                      _uiManager.ClosePanel(UIIDs.RevivePopup);
                                                  },
                                                  onCancel: () =>
                                                  {
                                                      AreYouSurePopupData areYouSurePopupData = new("Are you sure you want to cancel? You will lose all your rewards.",
                                                                                                    onConfirm: () =>
                                                                                                    {
                                                                                                        _temporaryRewardBarController.ClearRewards();

                                                                                                        _uiManager.ClosePanel(UIIDs.AreYouSurePopup);
                                                                                                    },
                                                                                                    onCancel: () =>
                                                                                                    {
                                                                                                        _uiManager.ClosePanel(UIIDs.AreYouSurePopup);
                                                                                                    });

                                                      ShowAreYouSurePopup(areYouSurePopupData);
                                                  });

            _uiManager.OpenPanel(UIIDs.RevivePopup, revivePopupData);
        }

        private void ShowAreYouSurePopup(AreYouSurePopupData data)
        {
            UIManager uiManager = ServiceLocator.Instance.Get<UIManager>();
            uiManager.OpenPanel(UIIDs.AreYouSurePopup, data);
        }

        private void OnProgress()
        {
            _currentZoneIndex++;

            if (_currentZoneIndex >= _zoneDatas.Count)
            {
                _temporaryRewardBarController.ClaimRewardsPermanently();
                return;
            }

            foreach (RouletteGameElementBase element in _rouletteGameElements)
            {
                element.OnProgress(_currentZoneIndex);
            }
        }

        private void UnsubscribeToEvents()
        {
            _eventManager.RemoveListener<CollectionAnimationFinishedEvent>(OnCollectionAnimationFinished);
        }

        private void OnDisable()
        {
            UnsubscribeToEvents();

            _exitButton.onClick.RemoveAllListeners();
        }
    }
}