using RewardSystem.Core;
using UnityEngine;

namespace RewardSystem.Rewards
{
    [CreateAssetMenu(fileName = "CurrencyReward", menuName = "RewardSystem/Rewards/CurrencyReward")]
    public class CurrencyReward : RewardBase
    {
        public override string GetValueText()
        {
            return Item.Amount.ToString();
        }
    }
}