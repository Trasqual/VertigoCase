using System.Collections.Generic;
using PoolingSystem;
using ServiceLocatorSystem;
using TMPro;
using UISystem.Animations;
using UISystem.RouletteGame.Data;
using UISystem.RouletteGame.RewardBar;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.RouletteGame.RouletteSpinner
{
    public class RouletteSpinnerController : RouletteGameElementBase
    {
        [SerializeField] private RewardVisual _rewardVisualPrefab;

        [SerializeField] private Button _spinButton;

        [SerializeField] private Image _rouletteImage;
        [SerializeField] private Image _rouletteIndicatorImage;

        [SerializeField] private TMP_Text _zoneTitleText;
        [SerializeField] private TMP_Text _zoneDescriptionText;

        private List<ZoneData> _zoneDatas = new();

        private ObjectPoolManager _poolManager;

        private WheelOfFortuneAnimation _spinAnimation;

        private RewardVisual[] _rewardVisuals;


        private int _currentZoneIndex;


        public override void Initialize(List<ZoneData> zoneDatas)
        {
            _poolManager = ServiceLocator.Instance.Get<ObjectPoolManager>();

            _spinAnimation = GetComponentInChildren<WheelOfFortuneAnimation>();

            _rewardVisuals = GetComponentsInChildren<RewardVisual>();

            _zoneDatas = zoneDatas;

            GenerateRewardVisuals();

            SetupAnimation();

            SetupVisuals();
            
            _spinButton.onClick.AddListener(OnSpinButtonClicked);
        }

        private void GenerateRewardVisuals()
        {
            _rewardVisuals = new RewardVisual[_zoneDatas[0].Rewards.Count];
            
            for (int i = 0; i < _zoneDatas[0].Rewards.Count; i++)
            {
                RewardVisual visual = _poolManager.GetObject(_rewardVisualPrefab, parent: _spinAnimation.transform);
                _rewardVisuals[i] = visual;
            }
        }


        private void OnSpinButtonClicked()
        {
            _spinAnimation.Play(OnSpinComplete);

            //TODO : OnSpinStarted event
        }

        private void OnSpinComplete()
        {
            int rewardIndex = _spinAnimation.GetClosestStopIndex();

            _zoneDatas[_currentZoneIndex].Rewards[rewardIndex].Claim();
        }

        private void SetupVisuals()
        {
            _rouletteImage.sprite = _zoneDatas[_currentZoneIndex].GetRouletteSprite();
            _rouletteIndicatorImage.sprite = _zoneDatas[_currentZoneIndex].GetRouletteIndicatorSprite();

            _zoneTitleText.SetText(_zoneDatas[_currentZoneIndex].ZoneTitle);
            _zoneDescriptionText.SetText(_zoneDatas[_currentZoneIndex].ZoneDescription);

            for (int i = 0; i < _rewardVisuals.Length; i++)
            {
                RewardVisual visual = _rewardVisuals[i];
                visual.Initialize(_zoneDatas[_currentZoneIndex].Rewards[i]);
            }
        }

        private void SetupAnimation()
        {
            _spinAnimation.SetStopCount(_zoneDatas[0].Rewards.Count);
        }

        public override void OnProgress(int currentIndex)
        {
            _currentZoneIndex = currentIndex;
            SetupAnimation();
            SetupVisuals();
        }
    }
}