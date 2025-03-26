using PoolingSystem;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace UISystem.RouletteGame.ZoneProgressBar
{
    public class ZoneProgressItem : MonoBehaviour, IPoolable
    {
        [field: SerializeField] public string PoolID { get; private set; }

        [SerializeField] private SpriteAtlas _atlas;

        public Component Component => this;

        private TMP_Text _text;

        private Image _backgroundImage;

        private ZoneData _zoneData;

        private int _index;

        public void Initialize(ZoneData zoneData, int zoneNo)
        {
            _text = GetComponentInChildren<TMP_Text>();
            _backgroundImage = GetComponent<Image>();

            _zoneData = zoneData;
            _index = zoneNo;

            UpdateVisuals();
        }

        private void UpdateVisuals()
        {
            _text.SetText(_index.ToString());
            _backgroundImage.sprite = _zoneData.GetBackgroundSprite();
        }

        public void OnSpawn()
        {
        }

        public void OnDespawn()
        {
        }
    }
}