using System.Collections.Generic;
using RewardSystem;
using UnityEngine;

namespace UISystem.RouletteGame
{
    public class ZoneData : ScriptableObject
    {
        [field: SerializeField] public ZoneType ZoneType { get; private set; }
        
        [field: SerializeField] public List<RewardData> RewardDatas { get; private set; }
    }
}