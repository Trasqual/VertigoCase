using RewardSystem.Core;
using UnityEngine;

namespace RewardSystem.ClaimStrategies
{
    public abstract class RewardClaimStrategyBase : ScriptableObject
    {
        public abstract void Execute(RewardBase reward, Vector3 position);
    }
}