using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UISystem.RouletteGame.Data;
using UnityEngine;

namespace UISystem.RouletteGame
{
    public abstract class RouletteGameElementBase : MonoBehaviour
    {
        public abstract void Initialize(List<ZoneData> zoneDatas);
        
        public abstract UniTask OnProgress(int currentIndex);

        public abstract void Clear();
    }
}