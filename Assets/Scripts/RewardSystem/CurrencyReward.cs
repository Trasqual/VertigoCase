using UnityEngine;

namespace RewardSystem
{
    [CreateAssetMenu(fileName = "CurrencyReward", menuName = "Rewards/CurrencyReward")]
    public class CurrencyReward : RewardBase
    {
        [field: SerializeField] public int Amount { get; private set; }

        public override void Claim()
        {
            Debug.Log("Claimed Currency Reward: " + Amount);
        }
    }
}