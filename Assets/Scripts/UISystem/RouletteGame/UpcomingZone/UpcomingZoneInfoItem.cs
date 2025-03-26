using PoolingSystem;
using TMPro;
using UnityEngine;

namespace UISystem.RouletteGame.UpcomingZone
{
    public class UpcomingZoneInfoItem : MonoBehaviour, IPoolable
    {
        [field: SerializeField] public string PoolID { get; private set; }

        [SerializeField] private TMP_Text _zoneTypeText;
        [SerializeField] private TMP_Text _zoneNoText;

        public Component Component => this;

        public void Initialize(ZoneType zoneType, int zoneNo)
        {
            _zoneTypeText.SetText(zoneType.ToString());
            _zoneNoText.SetText(zoneNo.ToString());
        }

        public void OnSpawn()
        {
        }

        public void OnDespawn()
        {
        }
    }
}