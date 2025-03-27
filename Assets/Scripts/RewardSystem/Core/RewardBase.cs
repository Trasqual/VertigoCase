using RewardSystem.ClaimStrategies;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.U2D;

namespace RewardSystem
{
    public abstract class RewardBase : ScriptableObject
    {
        [field: SerializeField] public string RewardName { get; private set; }

        [SerializeField] private string _iconName;

        [SerializeField] private SpriteAtlas _atlas;

        [FormerlySerializedAs("_rewardClaimStrategy")][SerializeField] private RewardClaimStrategyBase _rewardClaimStrategyBase;

        public Sprite Icon => _atlas.GetSprite(_iconName);

        public abstract string GetValueText();

        public virtual void Claim()
        {
            _rewardClaimStrategyBase.Execute(this);
        }
    }
}