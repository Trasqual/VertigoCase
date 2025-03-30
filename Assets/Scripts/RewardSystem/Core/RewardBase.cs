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

        [SerializeField] private RewardClaimStrategyBase _rewardTemporaryClaimStrategy;

        [SerializeField] private RewardClaimStrategyBase _rewardPermanentClaimStrategy;

        public Sprite Icon => _atlas.GetSprite(_iconName);

        public abstract string GetValueText();

        public virtual void ClaimTemporary(Vector3 position)
        {
            _rewardTemporaryClaimStrategy.Execute(this, position);
        }

        public virtual void ClaimPermanent(Vector3 position)
        {
            _rewardPermanentClaimStrategy.Execute(this, position);
        }
    }
}