using PoolingSystem;
using RewardSystem.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.RewardBar
{
    public class RewardVisual : MonoBehaviour, IPoolable
    {
        [SerializeField] private TMP_Text _valueText;

        [SerializeField] private Image _rewardIcon;
        [SerializeField] private Image _backgroundEffect;

        [field: SerializeField] public string PoolID { get; private set; }
        public Component Component => this;

        public RewardBase Reward { get; private set; }

        public void Initialize(RewardBase reward)
        {
            Reward = reward;
            _valueText.SetText(reward.GetValueText());
            _rewardIcon.sprite = reward.Item.ItemData.Icon;

            if (reward.BackgroundSprite != null)
            {
                _backgroundEffect.gameObject.SetActive(true);
                _backgroundEffect.sprite = reward.BackgroundSprite;
                _backgroundEffect.color = reward.BackgroundColor;
            }
            else
            {
                _backgroundEffect.gameObject.SetActive(false);
            }
        }

        public void SetValueText(string value)
        {
            _valueText.SetText(value);
        }

        public void OnSpawn()
        {
        }

        public void OnDespawn()
        {
        }
    }
}