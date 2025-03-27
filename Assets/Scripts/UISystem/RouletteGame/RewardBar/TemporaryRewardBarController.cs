using System.Collections.Generic;
using PoolingSystem;
using RewardSystem;
using ServiceLocatorSystem;
using UISystem.RouletteGame.Data;
using UnityEngine;
using UnityEngine.Serialization;

namespace UISystem.RouletteGame.RewardBar
{
    public class TemporaryRewardBarController : RouletteGameElementBase
    {
        [FormerlySerializedAs("_temporaryRewardVisualPrefab")][SerializeField] private RewardVisual _rewardVisualPrefab;
        [SerializeField] private Transform _content;

        private List<RewardVisual> _rewards = new();

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
            RewardVisual rewardVisual = _objectPoolManager.GetObject(_rewardVisualPrefab, parent: _content);
            rewardVisual.Initialize(reward);
            _rewards.Add(rewardVisual);
        }
    }
}