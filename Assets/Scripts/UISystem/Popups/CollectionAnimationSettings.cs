using DG.Tweening;
using UnityEngine;

namespace UISystem.Popups
{
    [CreateAssetMenu(fileName = "CollectionAnimationSettings", menuName = "Settings/UI/CollectionAnimationSettings")]
    public class CollectionAnimationSettings : ScriptableObject
    {
        [Header("Delay")]
        public float MinStartDelay = 0.1f;
        public float MaxStartDelay = 0.3f;

        [Header("Spread")]
        public float MinSpreadDistance = 50f;
        public float MaxSpreadDistance = 150f;
        public float SpreadDuration = 0.7f;
        public Ease SpreadEase = Ease.OutQuad;

        [Header("MoveToTarget")]
        public float MoveToTargetDuration = 0.5f;
        public Ease MoveToTargetEase = Ease.OutQuad;
    }
}