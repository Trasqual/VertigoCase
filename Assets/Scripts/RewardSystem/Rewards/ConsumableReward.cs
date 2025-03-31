using RewardSystem.Core;
using UnityEngine;

namespace RewardSystem.Rewards
{
    [CreateAssetMenu(fileName = "ConsumableReward", menuName = "RewardSystem/Rewards/ConsumableReward")]
    public class ConsumableReward : RewardBase
    {
        public override string GetValueText()
        {
            return Item.Amount.ToString();
        }
    }
}