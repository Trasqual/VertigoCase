using UnityEngine;
using UnityEngine.U2D;

namespace ItemSystem
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "ItemSystem/ItemData")]
    public class ItemData : ScriptableObject
    {
        [field: SerializeField] public string ItemId { get; private set; }
        [field: SerializeField] public string ItemName { get; private set; }

        [field: SerializeField] public bool IsStackable { get; private set; }

        [SerializeField] public string _iconName;

        [SerializeField] private SpriteAtlas _atlas;

        public Sprite Icon => _atlas.GetSprite(_iconName);
    }
}