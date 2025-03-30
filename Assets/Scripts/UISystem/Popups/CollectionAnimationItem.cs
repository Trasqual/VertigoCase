using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using PoolingSystem;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using Sequence = DG.Tweening.Sequence;

namespace UISystem.Popups
{
    [RequireComponent(typeof(Image))]
    public class CollectionAnimationItem : MonoBehaviour, IPoolable
    {
        [field: SerializeField] public string PoolID { get; private set; }

        private Vector3 _endPosition;

        private Image _image;

        private CollectionAnimationSettings _settings;

        private Sequence _sequence;

        public Component Component => this;

        public void Initialize(Vector3 endPosition, Sprite icon, CollectionAnimationSettings settings)
        {
            _image = GetComponent<Image>();
            
            _endPosition = endPosition;
            _settings = settings;
            _image.sprite = icon;
        }

        public async UniTask Animate(CancellationToken token)
        {
            transform.localScale = Vector3.zero;

            float randomSpreadX = Random.Range(_settings.MinSpreadDistance, _settings.MaxSpreadDistance);
            float randomSpreadY = Random.Range(_settings.MinSpreadDistance, _settings.MaxSpreadDistance);

            Vector2 randomPositionNormalized = Random.insideUnitCircle;
            Vector2 randomPosition = new(randomPositionNormalized.x * randomSpreadX, randomPositionNormalized.y * randomSpreadY);
            Vector3 spreadPosition = transform.position + new Vector3(randomPosition.x, randomPosition.y, 0);

            _sequence = DOTween.Sequence();
            _sequence.AppendInterval(Random.Range(_settings.MinStartDelay, _settings.MaxStartDelay));
            _sequence.Append(transform.DOMove(spreadPosition, _settings.SpreadDuration).SetEase(_settings.SpreadEase));
            _sequence.Join(transform.DOScale(Vector3.one, _settings.SpreadDuration));
            _sequence.Append(transform.DOMove(_endPosition, _settings.MoveToTargetDuration).SetEase(_settings.MoveToTargetEase));
            try
            {
                await _sequence.ToUniTask(cancellationToken: token);
            }
            catch (OperationCanceledException)
            {
                //canceled
            }
        }

        private void OnDisable()
        {
            _sequence.Kill();
        }

        public void OnSpawn()
        {
        }

        public void OnDespawn()
        {
        }
    }
}