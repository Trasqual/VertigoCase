using RewardSystem;
using UnityEngine;

namespace EventSystem
{
    public struct TemporaryRewardCollectedEvent
    {
        public RewardBase Reward;

        public Vector3 Position;

        public TemporaryRewardCollectedEvent(RewardBase reward, Vector3 position)
        {
            Reward = reward;
            Position = position;
        }
    }

    public struct CollectionAnimationFinishedEvent
    {
    }
}