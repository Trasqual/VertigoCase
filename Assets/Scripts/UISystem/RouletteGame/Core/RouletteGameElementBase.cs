using System.Collections.Generic;
using UISystem.RouletteGame.Data;
using UnityEngine;

namespace UISystem.RouletteGame
{
    public abstract class RouletteGameElementBase : MonoBehaviour
    {
        public abstract void Initialize(List<ZoneData> zoneDatas);
        public abstract void OnProgress(int currentIndex);
    }
}