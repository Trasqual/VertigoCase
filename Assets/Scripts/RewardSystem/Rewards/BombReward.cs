using RewardSystem.Core;
using UnityEngine;

namespace RewardSystem.Rewards
{
    [CreateAssetMenu(fileName = "Bomb", menuName = "RewardSystem/Rewards/Bomb")]
    public class BombReward : RewardBase
    {
        public override string GetValueText()
        {
            return "";
        }
    }
}