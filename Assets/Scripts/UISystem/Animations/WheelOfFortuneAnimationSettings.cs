using UnityEngine;

namespace UISystem.Animations
{
    [CreateAssetMenu(fileName = "WheelOfFortuneAnimationSettings", menuName = "Settings/UI/WheelOfFortuneAnimationSettings")]
    public class WheelOfFortuneAnimationSettings : ScriptableObject
    {
        public float SpinDuration = 2f;
        public float MinSpinSpeed = 90f;
        public float MaxSpinSpeed = 1440f;
        public float SnapSpeed = 50f;
        public bool Clockwise = true;
    }
}