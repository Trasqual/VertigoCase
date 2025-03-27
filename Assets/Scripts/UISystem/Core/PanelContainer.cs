using System.Collections.Generic;
using UnityEngine;

namespace UISystem.Core
{
    [CreateAssetMenu(fileName = "PanelContainer", menuName = "UISystem/PanelContainer")]
    public class PanelContainer : ScriptableObject
    {
        public List<UIPanelBase> Panels = new();
    }
}