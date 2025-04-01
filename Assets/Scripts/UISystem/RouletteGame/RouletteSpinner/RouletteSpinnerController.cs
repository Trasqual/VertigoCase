using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using PoolingSystem;
using ServiceLocatorSystem;
using TMPro;
using UISystem.Animations;
using UISystem.RewardBar;
using UISystem.RouletteGame.Data;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.RouletteGame.RouletteSpinner
{
    public class RouletteSpinnerController : RouletteGameElementBase
    {
        [SerializeField] private RewardVisual _rewardVisualPrefab;

        [SerializeField] private Image _rouletteImage;
        [SerializeField] private Image _rouletteIndicatorImage;

        [SerializeField] private TMP_Text _zoneTitleText;
        [SerializeField] private TMP_Text _zoneDescriptionText;

        [SerializeField] private WheelOfFortuneSpawnPunchAnimationSettings _punchAnimationSettings;

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
        }

        public void ApplySettings(WheelOfFortuneSpinAnimationSettings wheelOfFortuneSpinAnimationSettings,
                                  WheelOfFortuneSpawnPunchAnimationSettings wheelOfFortuneSpawnPunchAnimationSettings)
        {
            _spinAnimation.ApplySettings(wheelOfFortuneSpinAnimationSettings);
            _punchAnimationSettings = wheelOfFortuneSpawnPunchAnimationSettings;
        }

        private void GenerateRewardVisuals()
        {
            _rewardVisuals = new RewardVisual[_zoneDatas[0].Rewards.Count];

            for (int i = 0; i < _zoneDatas[0].Rewards.Count; i++)
            {
                RewardVisual visual = _poolManager.GetObject(_rewardVisualPrefab, parent: _spinAnimation.transform);
                visual.transform.localScale = Vector3.one;
                _rewardVisuals[i] = visual;
            }
        }

        public void Spin()
        {
            _spinAnimation.Play(OnSpinComplete);
        }

        private void OnSpinComplete()
        {
            int rewardIndex = _spinAnimation.GetClosestStopIndex();

            _zoneDatas[_currentZoneIndex].Rewards[rewardIndex].Claim(_rewardVisuals[rewardIndex].transform.position);
        }

        private void SetupVisuals()
        {
            _rouletteImage.transform.localRotation = Quaternion.identity;

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

        public override async UniTask OnProgress(int currentIndex)
        {
            _currentZoneIndex = currentIndex;
            SetupAnimation();
            SetupVisuals();

            await AnimateNewRewards();
        }

        private async UniTask AnimateNewRewards()
        {
            foreach (RewardVisual visual in _rewardVisuals)
            {
                _ = visual.transform.DOPunchScale(_punchAnimationSettings.Punch, _punchAnimationSettings.Duration, _punchAnimationSettings.Vibrato,
                                                  _punchAnimationSettings.Elasticity);
                await UniTask.Delay(_punchAnimationSettings.DelayBetweenRewards);
            }
        }

        public override void Clear()
        {
            _currentZoneIndex = 0;

            foreach (RewardVisual visual in _rewardVisuals)
            {
                _poolManager.ReleaseObject(visual);
            }

            _rewardVisuals = null;
        }
    }
}