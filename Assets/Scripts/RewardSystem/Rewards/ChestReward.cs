using RewardSystem.Core;
using UnityEngine;

namespace RewardSystem.Rewards
{
    [CreateAssetMenu(fileName = "ChestReward", menuName = "RewardSystem/Rewards/ChestReward")]
    public class ChestReward : RewardBase
    {
        public override string GetValueText()
        {
            return Item.ItemData.ItemName;
        }
    }
}