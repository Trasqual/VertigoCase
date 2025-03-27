using UnityEngine;

namespace RewardSystem.Rewards
{
    [CreateAssetMenu(fileName = "CustomizationReward", menuName = "RewardSystem/Rewards/CustomizationReward")]
    public class CustomizationReward : ItemRewardBase
    {
        public override string GetValueText()
        {
            return "";
        }
    }
}