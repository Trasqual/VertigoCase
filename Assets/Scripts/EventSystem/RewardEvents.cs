using RewardSystem;
using RewardSystem.Core;
using UnityEngine;

namespace EventSystem
{
    public struct RewardClaimedEvent
    {
        public RewardBase Reward;

        public Vector3 Position;

        public RewardClaimedEvent(RewardBase reward, Vector3 position)
        {
            Reward = reward;
            Position = position;
        }
    }

    public struct CollectionAnimationFinishedEvent
    {
    }
}