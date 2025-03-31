using RewardSystem.Core;
using UnityEngine;

namespace RewardSystem.Rewards
{
    [CreateAssetMenu(fileName = "UpgradeReward", menuName = "RewardSystem/Rewards/UpgradeReward")]
    public class UpgradeReward : RewardBase
    {
        public override string GetValueText()
        {
            return Item.Amount.ToString();
        }
    }
}