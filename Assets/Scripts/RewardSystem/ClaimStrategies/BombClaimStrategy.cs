using EventSystem;
using ServiceLocatorSystem;
using UnityEngine;

namespace RewardSystem.ClaimStrategies
{
    [CreateAssetMenu(fileName = "BombClaimStrategy", menuName = "RewardSystem/ClaimStrategies/BombClaimStrategy")]
    public class BombClaimStrategy : RewardClaimStrategyBase
    {
        public override void Execute(RewardBase reward, Vector3 position)
        {
            EventManager eventManager = ServiceLocator.Instance.Get<EventManager>();

            eventManager.TriggerEvent<BombClaimedEvent>(new BombClaimedEvent());
        }
    }
}