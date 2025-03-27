using UnityEngine;

namespace RewardSystem.Rewards
{
    [CreateAssetMenu(fileName = "CurrencyReward", menuName = "RewardSystem/Rewards/CurrencyReward")]
    public class CurrencyReward : RewardBase
    {
        [field: SerializeField] public int Amount { get; private set; }

        public override string GetValueText()
        {
            return Amount.ToString();
        }
    }
}