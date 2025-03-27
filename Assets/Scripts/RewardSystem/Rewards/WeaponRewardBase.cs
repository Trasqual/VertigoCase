using UnityEngine;

namespace RewardSystem.Rewards
{
    [CreateAssetMenu(fileName = "WeaponReward", menuName = "RewardSystem/Rewards/WeaponReward")]
    public class WeaponRewardBase : ItemRewardBase
    {
        public override string GetValueText()
        {
            return "";
        }
    }
}