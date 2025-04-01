using UnityEngine;

namespace UISystem.Animations
{
    [CreateAssetMenu(fileName = "WheelOfFortuneSpinAnimationSettings", menuName = "Settings/UI/WheelOfFortuneSpinAnimationSettings")]
    public class WheelOfFortuneSpinAnimationSettings : ScriptableObject
    {
        public float SpinDuration = 2f;
        public float MinSpinSpeed = 90f;
        public float MaxSpinSpeed = 1440f;
        public float SnapSpeed = 50f;
        public bool Clockwise = true;
    }
}