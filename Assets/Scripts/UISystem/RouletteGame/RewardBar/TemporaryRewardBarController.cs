using System.Collections.Generic;
using EventSystem;
using PoolingSystem;
using RewardSystem;
using ServiceLocatorSystem;
using UnityEngine;

namespace UISystem.RouletteGame.RewardBar
{
    public class TemporaryRewardBarController : MonoBehaviour
    {
        [SerializeField] private RewardVisual _rewardVisualPrefab;
        [SerializeField] private Transform _content;

        private List<RewardVisual> _rewards = new();

        private ObjectPoolManager _objectPoolManager;
        private EventManager _eventManager;

        public void Initialize()
        {
            _objectPoolManager = ServiceLocator.Instance.Get<ObjectPoolManager>();
            _eventManager = ServiceLocator.Instance.Get<EventManager>();

            _eventManager.AddListener<TemporaryRewardCollectedEvent>(OnRewardCollected);
        }

        private void OnRewardCollected(object eventData)
        {
            if (eventData is not TemporaryRewardCollectedEvent temporaryRewardCollectedEvent)
            {
                return;
            }

            AddReward(temporaryRewardCollectedEvent.Reward);
            
            //TODO: Play collection animation and set reward text value
            
            _eventManager.TriggerEvent<CollectionAnimationFinishedEvent>();
        }

        private void AddReward(RewardBase reward)
        {
            RewardVisual rewardVisual = _objectPoolManager.GetObject(_rewardVisualPrefab, parent: _content);
            rewardVisual.Initialize(reward);
            //rewardVisual.SetValueText("0");
            _rewards.Add(rewardVisual);
        }

        private void OnDisable()
        {
            _eventManager.RemoveListener<TemporaryRewardCollectedEvent>(OnRewardCollected);
        }
    }
}