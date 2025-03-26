using System.Collections.Generic;
using PoolingSystem;
using RewardSystem;
using ServiceLocatorSystem;
using UISystem.RouletteGame.Data;
using UnityEngine;

namespace UISystem.RouletteGame.RewardBar
{
    public class TemporaryRewardBarController : RouletteGameElementBase
    {
        [SerializeField] private TemporaryRewardVisual _temporaryRewardVisualPrefab;
        [SerializeField] private Transform _content;

        private List<TemporaryRewardVisual> _rewards;

        private ObjectPoolManager _objectPoolManager;

        public override void Initialize(List<ZoneData> zoneDatas)
        {
            _objectPoolManager = ServiceLocator.Instance.Get<ObjectPoolManager>();
        }

        public override void OnProgress(int currentIndex)
        {
        }

        public void AddReward(RewardBase reward)
        {
            TemporaryRewardVisual temporaryRewardVisual = _objectPoolManager.GetObject(_temporaryRewardVisualPrefab, parent: _content);
            temporaryRewardVisual.Initialize(reward);
            _rewards.Add(temporaryRewardVisual);
        }
    }
}