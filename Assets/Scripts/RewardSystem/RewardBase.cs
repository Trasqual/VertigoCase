using UnityEngine;
using UnityEngine.U2D;

namespace RewardSystem
{
    public abstract class RewardBase : ScriptableObject
    {
        [field: SerializeField] public string RewardName { get; private set; }

        [SerializeField] private string _iconName;

        [SerializeField] private SpriteAtlas _atlas;

        public Sprite Icon => _atlas.GetSprite(_iconName);

        public abstract void Claim();
    }
}