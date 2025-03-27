using UnityEngine;

namespace RewardSystem.Rewards
{
    [CreateAssetMenu(fileName = "UpgradeReward", menuName = "RewardSystem/Rewards/UpgradeReward")]
    public class UpgradeReward : RewardBase
    {
        [field: SerializeField] public int Amount { get; private set; }

        public override string GetValueText()
        {
            return Amount.ToString();
        }
    }
}