using RewardSystem.Core;
using UnityEngine;

namespace RewardSystem.ClaimStrategies
{
    [CreateAssetMenu(fileName = "DebugRewardClaimStrategy", menuName = "RewardSystem/ClaimStrategies/DebugRewardClaimStrategy")]
    public class DebugRewardClaimStrategy : RewardClaimStrategyBase
    {
        public override void Execute(RewardBase reward, Vector3 position)
        {
            Debug.Log("Claimed Reward: " + reward.Item.ItemData.ItemName + " at position: " + position);
        }
    }
}