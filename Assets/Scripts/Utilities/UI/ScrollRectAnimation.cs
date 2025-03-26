using System;
using DG.Tweening;
using UISystem.RouletteGame.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities.UI
{
    [RequireComponent(typeof(ScrollRect))]
    public class ScrollRectAnimation : MonoBehaviour
    {
        [SerializeField] private ScrollAnimationSettings _scrollAnimationSettings;

        private ScrollRect _scrollRect;

        private LayoutGroup _layoutGroup;

        private Transform _content;

        private void Awake()
        {
            _scrollRect = GetComponent<ScrollRect>();
            _content = _scrollRect.content;
        }

        public void ScrollToObject(int index, Action onComplete = null)
        {
            if (index < 0 || index >= _scrollRect.content.childCount || _scrollRect.content.childCount == 0)
            {
                return;
            }

            _layoutGroup ??= _content.GetComponent<LayoutGroup>();

            if (_layoutGroup == null)
            {
                Debug.Log("There is no layout group on the content object.");
                return;
            }

            RectTransform childRect = (RectTransform)_content.GetChild(0);
            float childSize = 0f;
            float spacing = 0f;
            float scrollPos = 0f;

            switch (_layoutGroup)
            {
                case HorizontalLayoutGroup horizontalLayoutGroup:
                    childSize = childRect.rect.width;
                    spacing = horizontalLayoutGroup.spacing;
                    scrollPos = index * (childSize + spacing);

                    DOVirtual.Float(_content.localPosition.x, -scrollPos, _scrollAnimationSettings.Duration,
                                    value => _content.localPosition = new Vector3(value, _content.localPosition.y, _content.localPosition.z))
                             .SetEase(_scrollAnimationSettings.Ease)
                             .OnComplete(() => onComplete?.Invoke());
                    break;

                case VerticalLayoutGroup verticalLayoutGroup:
                    childSize = childRect.rect.height;
                    spacing = verticalLayoutGroup.spacing;
                    scrollPos = index * (childSize + spacing);

                    DOVirtual.Float(_content.localPosition.y, -scrollPos, _scrollAnimationSettings.Duration,
                                    value => _content.localPosition = new Vector3(_content.localPosition.x, value, _content.localPosition.z))
                             .SetEase(_scrollAnimationSettings.Ease)
                             .OnComplete(() => onComplete?.Invoke());
                    break;
            }
        }
    }
}