using UnityEngine;

namespace RewardSystem.ClaimStrategies
{
    [CreateAssetMenu(fileName = "DebugRewardClaimStrategy", menuName = "RewardSystem/ClaimStrategies/DebugRewardClaimStrategy")]
    public class DebugRewardClaimStrategy : RewardClaimStrategyBase
    {
        public override void Execute(RewardBase reward)
        {
            Debug.Log("Claimed Reward: " + reward.RewardName);
        }
    }
}