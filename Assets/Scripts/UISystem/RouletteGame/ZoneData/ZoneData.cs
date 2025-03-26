using System.Collections.Generic;
using RewardSystem;
using UnityEngine;

namespace UISystem.RouletteGame
{
    [CreateAssetMenu(fileName = "ZoneData", menuName = "RouletteGame/ZoneData")]
    public class ZoneData : ScriptableObject
    {
        [field: SerializeField] public ZoneType ZoneType { get; private set; }
        
        [field: SerializeField] public string ZoneTitle { get; private set; }
        
        [field: SerializeField] public string ZoneDescription { get; private set; }
        
        [field: SerializeField] public List<RewardData> RewardDatas { get; private set; }
    }
}