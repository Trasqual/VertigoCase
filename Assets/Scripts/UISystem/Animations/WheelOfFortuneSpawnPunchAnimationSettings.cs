using UnityEngine;

namespace UISystem.Animations
{
    [CreateAssetMenu(fileName = "WheelOfFortuneSpawnPunchAnimationSettings", menuName = "Settings/UI/WheelOfFortuneSpawnPunchAnimationSettings")]
    public class WheelOfFortuneSpawnPunchAnimationSettings : ScriptableObject
    {
        public Vector3 Punch = Vector3.one;
        public float Duration = 1f;
        public int Vibrato = 1;
        public float Elasticity = 0.5f;
        public int DelayBetweenRewards = 200;
    }
}