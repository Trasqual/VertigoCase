using System.Collections.Generic;
using System.Linq;
using EventSystem;
using ItemSystem;
using PoolingSystem;
using RewardSystem;
using RewardSystem.Core;
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

        private Dictionary<string, Item> _temporaryItemDictionary = new();

        private List<RewardVisual> _rewardVisuals = new();

        private ObjectPoolManager _objectPoolManager;
        private EventManager _eventManager;

        public void Initialize()
        {
            _objectPoolManager = ServiceLocator.Instance.Get<ObjectPoolManager>();
            _eventManager = ServiceLocator.Instance.Get<EventManager>();

            _eventManager.AddListener<RewardClaimedEvent>(OnRewardCollected);
        }

        private void OnRewardCollected(object eventData)
        {
            if (eventData is not RewardClaimedEvent rewardClaimedEvent)
            {
                return;
            }

            RewardVisual visual = AddReward(rewardClaimedEvent.Reward);

            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)_content);

            UIManager uiManager = ServiceLocator.Instance.Get<UIManager>();
            uiManager.OpenPanel(UIIDs.CollectionAnimationPopup, new CollectionAnimationPopupData(rewardClaimedEvent.Position,
                                                                                                 visual.transform.position,
                                                                                                 rewardClaimedEvent.Reward.Item.ItemData.Icon,
                                                                                                 6,
                                                                                                 _animationSettings,
                                                                                                 () => OnAnimationComplete(visual)));
        }

        private void OnAnimationComplete(RewardVisual rewardVisual)
        {
            int currentAmount = _temporaryItemDictionary[rewardVisual.Reward.Item.ItemData.ItemId].Amount;
            rewardVisual.SetValueText(currentAmount.ToString());
            _eventManager.TriggerEvent<CollectionAnimationFinishedEvent>();
        }

        private RewardVisual AddReward(RewardBase reward)
        {
            if (!reward.Item.ItemData.IsStackable || !_temporaryItemDictionary.ContainsKey(reward.Item.ItemData.ItemId))
            {
                return GenerateVisual(reward);
            }

            _temporaryItemDictionary[reward.Item.ItemData.ItemId].AddAmount(reward.Item.Amount);

            RewardVisual existingVisual = _rewardVisuals.FirstOrDefault(v => v.Reward.Item.ItemData.ItemId == reward.Item.ItemData.ItemId);

            return existingVisual;
        }

        private RewardVisual GenerateVisual(RewardBase reward)
        {
            _temporaryItemDictionary.Add(reward.Item.ItemData.ItemId, reward.Item.Copy());

            RewardVisual rewardVisual = _objectPoolManager.GetObject(_rewardVisualPrefab, parent: _content);
            rewardVisual.Initialize(reward);
            rewardVisual.SetValueText("");
            _rewardVisuals.Add(rewardVisual);

            return rewardVisual;
        }

        public void ClaimRewardsPermanently()
        {
            foreach (Item item in _temporaryItemDictionary.Values)
            {
                Debug.Log("Claimed Item: " + item.ItemData.ItemName + " x" + item.Amount);
            }

            _rewardVisuals.Clear();
        }

        public void ClearRewards()
        {
            foreach (RewardVisual rewardVisual in _rewardVisuals)
            {
                _objectPoolManager.ReleaseObject(rewardVisual);
            }

            _rewardVisuals.Clear();

            _temporaryItemDictionary.Clear();
        }

        private void OnDisable()
        {
            _eventManager?.RemoveListener<RewardClaimedEvent>(OnRewardCollected);
        }
    }
}