using PoolingSystem;
using RewardSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.RouletteGame.RewardBar
{
    public class RewardVisual : MonoBehaviour,IPoolable
    {
        [SerializeField] private TMP_Text ValueText;
        [SerializeField] private Image RewardIcon;
        
        [field: SerializeField] public string PoolID { get; private set; }
        public Component Component => this;

        public void Initialize(RewardBase reward)
        {
            ValueText.SetText(reward.GetValueText());
            RewardIcon.sprite = reward.Icon;
        }

        public void SetValueText(string value)
        {
            ValueText.SetText(value);
        }
        
        public void OnSpawn()
        {
            
        }

        public void OnDespawn()
        {
        }
    }
}