using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using PoolingSystem;
using ServiceLocatorSystem;
using UISystem.Core;
using UnityEngine;

namespace UISystem.Popups
{
    public class CollectionAnimationPopup : UIPanelBase
    {
        [SerializeField] private CollectionAnimationItem _itemPrefab;

        private List<CollectionAnimationItem> _items = new();

        private CollectionAnimationPopupData _data;

        private ObjectPoolManager _poolManager;

        private CancellationToken _cancellationToken;
        public override string GetPanelID() => UIIDs.CollectionAnimationPopup;

        public override void ApplyData(object data)
        {
            if (data is not CollectionAnimationPopupData animationData)
            {
                Debug.Log($"Wrong data {data.GetType()} sent for UI {GetPanelID()}");
                return;
            }

            _data = animationData;
        }

        public override void Show()
        {
            _cancellationToken = this.GetCancellationTokenOnDestroy();

            _poolManager = ServiceLocator.Instance.Get<ObjectPoolManager>();

            for (int i = 0; i < _data.Count; i++)
            {
                CollectionAnimationItem item = _poolManager.GetObject(_itemPrefab, position: _data.StartPosition, parent: transform);
                item.Initialize(_data.EndPosition, _data.Icon, _data.Settings);
                _items.Add(item);
            }

            Play().Forget();
        }

        private async UniTaskVoid Play()
        {
            UniTask[] tasks = new UniTask[_items.Count];

            for (int i = 0; i < _items.Count; i++)
            {
                CollectionAnimationItem item = _items[i];
                tasks[i] = item.Animate(_cancellationToken);
            }

            await UniTask.WhenAll(tasks);

            _data.OnAnimationComplete?.Invoke();

            UIManager uiManager = ServiceLocator.Instance.Get<UIManager>();
            uiManager.ClosePanel(GetPanelID());
        }

        public override void Hide()
        {
            ResetSelf();
        }

        private void ResetSelf()
        {
            _cancellationToken = CancellationToken.None;

            for (int i = 0; i < _items.Count; i++)
            {
                CollectionAnimationItem item = _items[i];
                item.gameObject.SetActive(false);
                _poolManager.ReleaseObject(item);
            }

            _items.Clear();
        }
    }

    public partial class UIIDs
    {
        public const string CollectionAnimationPopup = "CollectionAnimationPopup";
    }

    public struct CollectionAnimationPopupData
    {
        public CollectionAnimationSettings Settings;

        public Vector3 StartPosition;
        public Vector3 EndPosition;

        public int Count;

        public Sprite Icon;

        public Action OnAnimationComplete;

        public CollectionAnimationPopupData(Vector3 startPosition,
                                            Vector3 endPosition,
                                            Sprite icon,
                                            int count,
                                            CollectionAnimationSettings settings,
                                            Action onAnimationComplete)
        {
            StartPosition = startPosition;
            EndPosition = endPosition;
            Icon = icon;
            Count = count;
            Settings = settings;
            OnAnimationComplete = onAnimationComplete;
        }
    }
}