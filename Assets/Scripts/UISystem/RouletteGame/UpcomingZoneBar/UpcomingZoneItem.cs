using PoolingSystem;
using TMPro;
using UISystem.RouletteGame.Data;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.RouletteGame.UpcomingZoneBar
{
    public class UpcomingZoneItem : MonoBehaviour, IPoolable
    {
        [field: SerializeField] public string PoolID { get; private set; }

        [SerializeField] private TMP_Text _zoneTypeText;
        [SerializeField] private TMP_Text _zoneNoText;
        
        [SerializeField] private Image _backgroundImage;
        
        public Component Component => this;

        public void Initialize(ZoneData zoneData, int zoneNo)
        {
            _zoneTypeText.SetText(zoneData.ZoneName);
            _zoneNoText.SetText(zoneNo.ToString());
            _backgroundImage.sprite = zoneData.GetBackgroundSprite();
        }

        public void OnSpawn()
        {
        }

        public void OnDespawn()
        {
        }
    }
}