using System;
using System.Collections.Generic;
using EventSystem;
using ServiceLocatorSystem;
using StateMachineSystem;
using UISystem.Animations;
using UISystem.Core;
using UISystem.Popups;
using UISystem.RouletteGame.Data;
using UISystem.RouletteGame.RewardBar;
using UISystem.RouletteGame.RouletteSpinner;
using UISystem.RouletteGame.ZoneProgressBar;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace UISystem.RouletteGame.Core
{
    public class RouletteGameController : MonoBehaviour
    {
        public Action OnExitGame;

        [field: Header("References")]
        [field: SerializeField] public Button ExitButton { get; private set; }

        [field: SerializeField] public Button SpinButton { get; private set; }

        [SerializeField] private List<RouletteGameElementBase> _rouletteGameElements = new();

        [SerializeField] private RouletteSpinnerController _spinnerController;

        [SerializeField] private ZoneProgressBarControllerBase _progressBarController;
        [SerializeField] private ZoneProgressBarControllerBase _currentProgressBarController;

        [SerializeField] private TemporaryRewardBarController _temporaryRewardBarController;

        [Header("Animation Settings")]
        [SerializeField] private ScrollAnimationSettings _scrollAnimationSettings;
        [SerializeField] private WheelOfFortuneAnimationSettings _wheelOfFortuneAnimationSettings;
        [SerializeField] private CollectionAnimationSettings _collectionAnimationSettings;

        [Header("Datas")]
        [SerializeField] private List<ZoneData> _zoneDatas = new();

        [Header("Bomb")]
        [SerializeField] private SpriteAtlas _atlas;
        [SerializeField] private string _bombSpriteName;
        public ZoneData CurrentZoneData => _zoneDatas[_currentZoneIndex];

        private StateMachine _stateMachine;
        private RouletteGameIdleState _idleState;
        private RouletteGameSpinningState _spinningState;
        private RouletteGameInactiveState _inactiveState;

        private EventManager _eventManager;

        private UIManager _uiManager;


        private int _currentZoneIndex;

        public void Initialize()
        {
            _eventManager = ServiceLocator.Instance.Get<EventManager>();

            _uiManager = ServiceLocator.Instance.Get<UIManager>();

            InitializeStateMachine();

            SubscribeToEvents();

            InitializeElements();

            ExitButton.onClick.AddListener(OnExitButtonClicked);
            SpinButton.onClick.AddListener(OnSpinStarted);
        }

        private void ApplyAnimationSettings()
        {
            _spinnerController.ApplySettings(_wheelOfFortuneAnimationSettings);
            _progressBarController.ApplySettings(_scrollAnimationSettings);
            _currentProgressBarController.ApplySettings(_scrollAnimationSettings);
            _temporaryRewardBarController.ApplySettings(_collectionAnimationSettings);
        }

        private void InitializeStateMachine()
        {
            _stateMachine = new StateMachine();
            _idleState = new RouletteGameIdleState(this);
            _spinningState = new RouletteGameSpinningState(this);
            _inactiveState = new RouletteGameInactiveState(this);
            _stateMachine.ChangeState(_idleState);
        }

        private void SubscribeToEvents()
        {
            _eventManager.AddListener<CollectionAnimationFinishedEvent>(OnCollectionAnimationFinished);
            _eventManager.AddListener<BombClaimedEvent>(OnBombClaimed);
        }

        private void OnSpinStarted()
        {
            _stateMachine.ChangeState(_spinningState);
            _spinnerController.Spin();
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
            ProgressToNextZone();
        }

        private void ProgressToNextZone()
        {
            _currentZoneIndex++;

            if (_currentZoneIndex >= _zoneDatas.Count)
            {
                _temporaryRewardBarController.ClaimRewardsPermanently();
                _stateMachine.ChangeState(_inactiveState);

                ExitGame();
                return;
            }

            foreach (RouletteGameElementBase element in _rouletteGameElements)
            {
                element.OnProgress(_currentZoneIndex);
            }
        }

        private void OnExitButtonClicked()
        {
            AreYouSurePopupData areYouSurePopupData = new("Are you sure you want to exit the roulette game?",
                                                          onConfirm: () =>
                                                          {
                                                              _temporaryRewardBarController.ClaimRewardsPermanently();
                                                              _uiManager.ClosePanel(UIIDs.AreYouSurePopup);
                                                              ExitGame();
                                                          },
                                                          onCancel: () =>
                                                          {
                                                              _uiManager.ClosePanel(UIIDs.AreYouSurePopup);
                                                              _stateMachine.ChangeState(_idleState);
                                                          });
            ShowAreYouSurePopup(areYouSurePopupData);
        }

        private void OnBombClaimed(object obj)
        {
            ShowRevivePopup();
        }

        private void ShowRevivePopup()
        {
            RevivePopupData revivePopupData = new("Oh no! The bomb is about to explode!",
                                                  icon: _atlas.GetSprite(_bombSpriteName),
                                                  reviveButtonText: "Revive",
                                                  cancelButtonText: "Quit",
                                                  onRevive: () =>
                                                  {
                                                      Debug.Log("Spent some coins to revive.");
                                                      _uiManager.ClosePanel(UIIDs.RevivePopup);
                                                      ProgressToNextZone();
                                                      _stateMachine.ChangeState(_idleState);
                                                  },
                                                  onCancel: () =>
                                                  {
                                                      RevivePopup revivePopup = _uiManager.GetActivePanel<RevivePopup>(UIIDs.RevivePopup);
                                                      revivePopup.PauseCountdown();
                                                      AreYouSurePopupData areYouSurePopupData = new("Are you sure you want to quit? You will lose all your rewards.",
                                                                                                    onConfirm: () =>
                                                                                                    {
                                                                                                        _uiManager.ClosePanel(UIIDs.AreYouSurePopup);
                                                                                                        _uiManager.ClosePanel(UIIDs.RevivePopup);
                                                                                                        ExitGame();
                                                                                                    },
                                                                                                    onCancel: () =>
                                                                                                    {
                                                                                                        _uiManager.ClosePanel(UIIDs.AreYouSurePopup);
                                                                                                        revivePopup.ResumeCountdown();
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

        private void ExitGame()
        {
            Clear();
            gameObject.SetActive(false);

            OnExitGame?.Invoke();
        }

        private void Clear()
        {
            foreach (RouletteGameElementBase element in _rouletteGameElements)
            {
                element.Clear();
            }

            _temporaryRewardBarController.Clear();
        }

        private void UnsubscribeToEvents()
        {
            _eventManager?.RemoveListener<CollectionAnimationFinishedEvent>(OnCollectionAnimationFinished);
            _eventManager?.RemoveListener<BombClaimedEvent>(OnBombClaimed);
        }

        private void OnDisable()
        {
            UnsubscribeToEvents();

            ExitButton.onClick.RemoveAllListeners();
        }
    }
}