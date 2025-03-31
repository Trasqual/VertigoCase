using EventSystem;
using RewardSystem.Core;
using ServiceLocatorSystem;
using UnityEngine;

namespace RewardSystem.ClaimStrategies
{
    [CreateAssetMenu(fileName = "EventTriggeringRewardClaimStrategy", menuName = "RewardSystem/ClaimStrategies/EventTriggeringRewardClaimStrategy")]
    public class EventTriggeringRewardClaimStrategy : RewardClaimStrategyBase
    {
        public override void Execute(RewardBase reward, Vector3 position)
        {
            EventManager eventManager = ServiceLocator.Instance.Get<EventManager>();

            eventManager.TriggerEvent<RewardClaimedEvent>(new RewardClaimedEvent(reward, position));
        }
    }
}