using DG.Tweening;
using UnityEngine;

namespace UISystem.RouletteGame.Data
{
    [CreateAssetMenu(fileName = "ScrollAnimationSettings", menuName = "Settings/UI/ScrollAnimationSettings")]
    public class ScrollAnimationSettings : ScriptableObject
    {
        [field: SerializeField] public Ease Ease { get; private set; }
        [field: SerializeField] public float Duration { get; private set; }
    }
}