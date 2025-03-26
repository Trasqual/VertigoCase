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

        [SerializeField] private SpriteAtlas _iconAtlas;

        public Component Component => this;

        private TMP_Text _text;

        private Image _backgroundImage;

        private ZoneType _zoneType;

        private int _index;

        public void Initialize(ZoneType type, int zoneNo)
        {
            _text = GetComponentInChildren<TMP_Text>();
            _backgroundImage = GetComponent<Image>();

            _zoneType = type;
            _index = zoneNo;

            UpdateVisuals();
        }

        private void UpdateVisuals()
        {
            _text.SetText(_index.ToString());
            _backgroundImage.sprite = _zoneType switch
            {
                    ZoneType.Regular => _iconAtlas.GetSprite("ui_card_panel_zone_white"),
                    ZoneType.Safe => _iconAtlas.GetSprite("ui_card_panel_zone_current"),
                    ZoneType.Super => _iconAtlas.GetSprite("ui_card_panel_zone_super"),
                    _ => _backgroundImage.sprite
            };
        }

        public void OnSpawn()
        {
        }

        public void OnDespawn()
        {
        }
    }
}