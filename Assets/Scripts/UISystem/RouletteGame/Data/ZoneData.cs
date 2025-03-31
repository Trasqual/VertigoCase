using System.Collections.Generic;
using RewardSystem.Core;
using UnityEngine;
using UnityEngine.U2D;

namespace UISystem.RouletteGame.Data
{
    [CreateAssetMenu(fileName = "ZoneData", menuName = "RouletteGame/ZoneData")]
    public class ZoneData : ScriptableObject
    {
        [field: SerializeField] public ZoneType ZoneType { get; private set; }
        
        [field: SerializeField] public string ZoneName { get; private set; }
        [field: SerializeField] public string ZoneTitle { get; private set; }
        [field: SerializeField] public string ZoneDescription { get; private set; }
        
        [field: SerializeField] public string ZoneBackgroundSpriteName { get; private set; }
        [field: SerializeField] public string ZoneRouletteSpriteName { get; private set; }
        [field: SerializeField] public string ZoneRouletteIndicatorSpriteName { get; private set; }
        
        [field: SerializeField] public SpriteAtlas Atlas { get; private set; }

        [field: SerializeField] public List<RewardBase> Rewards { get; private set; }
        
        public Sprite GetRouletteSprite() => Atlas.GetSprite(ZoneRouletteSpriteName);
        
        public Sprite GetRouletteIndicatorSprite() => Atlas.GetSprite(ZoneRouletteIndicatorSpriteName);

        public Sprite GetBackgroundSprite() => Atlas.GetSprite(ZoneBackgroundSpriteName);
    }
}