using RewardSystem.Core;
using UnityEngine;

namespace RewardSystem.Rewards
{
    [CreateAssetMenu(fileName = "WeaponReward", menuName = "RewardSystem/Rewards/WeaponReward")]
    public class WeaponRewardBase : RewardBase
    {
        public override string GetValueText()
        {
            return Item.ItemData.ItemName;;
        }
    }
}