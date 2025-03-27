using RewardSystem;

namespace EventSystem
{
    public struct TemporaryRewardCollectedEvent
    {
        public RewardBase Reward;

        public TemporaryRewardCollectedEvent(RewardBase reward)
        {
            Reward = reward;
        }
    }
    
    public struct CollectionAnimationFinishedEvent
    {
    
    }
}