using UnityEngine;

namespace RewardSystem.Rewards
{
    public abstract class ItemRewardBase : RewardBase
    {
        [field: SerializeField] public string ItemID { get; private set; }
    }
}