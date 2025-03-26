using UnityEngine;

namespace RewardSystem
{
    [CreateAssetMenu(fileName = "CustomizationReward", menuName = "Rewards/CustomizationReward")]
    public class CustomizationRewardBase : ItemRewardBase
    {
        public override string GetValueText()
        {
            return "";
        }
    }
}