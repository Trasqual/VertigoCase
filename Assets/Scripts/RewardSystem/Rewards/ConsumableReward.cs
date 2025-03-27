using UnityEngine;

namespace RewardSystem.Rewards
{
    [CreateAssetMenu(fileName = "ConsumableReward", menuName = "RewardSystem/Rewards/ConsumableReward")]
    public class ConsumableReward : ItemRewardBase
    {
        [field: SerializeField] public int Amount { get; private set; }
        
        public override string GetValueText()
        {
            return Amount.ToString();
        }
    }
}