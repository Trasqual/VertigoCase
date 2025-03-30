using EventSystem;
using ServiceLocatorSystem;
using UnityEngine;

namespace RewardSystem.ClaimStrategies
{
    [CreateAssetMenu(fileName = "TemporaryRewardClaimStrategy", menuName = "RewardSystem/ClaimStrategies/TemporaryRewardClaimStrategy")]
    public class TemporaryRewardClaimStrategy : RewardClaimStrategyBase
    {
        public override void Execute(RewardBase reward, Vector3 position)
        {
            EventManager eventManager = ServiceLocator.Instance.Get<EventManager>();

            eventManager.TriggerEvent<TemporaryRewardCollectedEvent>(new TemporaryRewardCollectedEvent(reward, position));
        }
    }
}