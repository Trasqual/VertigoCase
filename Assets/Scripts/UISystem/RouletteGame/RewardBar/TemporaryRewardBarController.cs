using System.Collections.Generic;
using EventSystem;
using PoolingSystem;
using RewardSystem;
using ServiceLocatorSystem;
using UISystem.Core;
using UISystem.Popups;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.RouletteGame.RewardBar
{
    public class TemporaryRewardBarController : MonoBehaviour
    {
        [SerializeField] private RewardVisual _rewardVisualPrefab;
        [SerializeField] private Transform _content;

        [SerializeField] private CollectionAnimationSettings _animationSettings;

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

            RewardVisual generatedVisual = AddReward(temporaryRewardCollectedEvent.Reward);

            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)_content);
            
            UIManager uiManager = ServiceLocator.Instance.Get<UIManager>();
            uiManager.OpenPanel(UIIDs.CollectionAnimationPopup, new CollectionAnimationPopupData(temporaryRewardCollectedEvent.Position,
                                                                                                 generatedVisual.transform.position,
                                                                                                 temporaryRewardCollectedEvent.Reward.Icon,
                                                                                                 6,
                                                                                                 _animationSettings,
                                                                                                 ()=>OnAnimationComplete(generatedVisual)));
        }

        private void OnAnimationComplete(RewardVisual rewardVisual)
        {
            rewardVisual.SetValueText(rewardVisual.Reward.GetValueText());
            _eventManager.TriggerEvent<CollectionAnimationFinishedEvent>();
        }

        private RewardVisual AddReward(RewardBase reward)
        {
            RewardVisual rewardVisual = _objectPoolManager.GetObject(_rewardVisualPrefab, parent: _content);
            rewardVisual.Initialize(reward);
            rewardVisual.SetValueText("");
            _rewards.Add(rewardVisual);

            return rewardVisual;
        }

        public void ClaimRewardsPermanently()
        {
            foreach (RewardVisual rewardVisual in _rewards)
            {
                rewardVisual.Reward.ClaimPermanent(rewardVisual.transform.position);
            }

            _rewards.Clear();
        }

        public void ClearRewards()
        {
            foreach (RewardVisual rewardVisual in _rewards)
            {
                _objectPoolManager.ReleaseObject(rewardVisual);
            }

            _rewards.Clear();
        }

        private void OnDisable()
        {
            _eventManager?.RemoveListener<TemporaryRewardCollectedEvent>(OnRewardCollected);
        }
    }
}