using UnityEngine;

namespace RewardSystem
{
    public abstract class ItemRewardBase : RewardBase
    {
        [field: SerializeField] public string ItemID { get; private set; }

        public override void Claim()
        {
            Debug.Log("Claimed Item Reward: " + ItemID);
        }
    }
}