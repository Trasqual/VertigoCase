using RewardSystem.Core;
using UnityEngine;

namespace RewardSystem.Rewards
{
    [CreateAssetMenu(fileName = "CustomizationReward", menuName = "RewardSystem/Rewards/CustomizationReward")]
    public class CustomizationReward : RewardBase
    {
        public override string GetValueText()
        {
            return Item.ItemData.ItemName;
        }
    }
}