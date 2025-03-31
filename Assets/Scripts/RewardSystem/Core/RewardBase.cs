using ItemSystem;
using RewardSystem.ClaimStrategies;
using UnityEngine;

namespace RewardSystem.Core
{
    public abstract class RewardBase : ScriptableObject
    {
        [field: SerializeField] public Item Item { get; private set; }

        [field: SerializeField] public Sprite BackgroundSprite { get; private set; }

        [field: SerializeField] public Color BackgroundColor { get; private set; }

        [SerializeField] private RewardClaimStrategyBase _rewardClaimStrategy;

        public abstract string GetValueText();

        public void Claim(Vector3 position)
        {
            _rewardClaimStrategy.Execute(this, position);
        }
    }
}