using UnityEngine;

namespace RewardSystem
{
    [CreateAssetMenu(fileName = "ConsumableReward", menuName = "Rewards/ConsumableReward")]
    public class ConsumableRewardBase : ItemRewardBase
    {
        [field: SerializeField] public int Amount { get; private set; }
        
        public override string GetValueText()
        {
            return Amount.ToString();
        }
    }
}