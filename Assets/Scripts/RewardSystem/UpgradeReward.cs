using UnityEngine;

namespace RewardSystem
{
    [CreateAssetMenu(fileName = "UpgradeReward", menuName = "Rewards/UpgradeReward")]
    public class UpgradeReward : RewardBase
    {
        [field: SerializeField] public int Amount { get; private set; }

        public override string GetValueText()
        {
            return Amount.ToString();
        }

        public override void Claim()
        {
            Debug.Log("Claimed UpgradeReward Reward: " + Amount);
        }
    }
}