using UnityEngine;

namespace RewardSystem
{
    [CreateAssetMenu(fileName = "WeaponReward", menuName = "Rewards/WeaponReward")]
    public class WeaponRewardBase : ItemRewardBase
    {
        public override string GetValueText()
        {
            return "";
        }
    }
}