using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities.UI
{
    public class CenteredScrollRect : ScrollRect
    {
        private LayoutGroup _layoutGroup;

        [ContextMenu("TestScrollTo5")]
        private void TestScrollTo5()
        {
            ScrollToObject(5, 1f);
        }

        public void ScrollToObject(int index, float duration)
        {
            if (index < 0 || index >= content.childCount || content.childCount == 0)
            {
                return;
            }

            _layoutGroup ??= content.GetComponent<LayoutGroup>();

            if (_layoutGroup == null)
            {
                Debug.Log("There is no layout group on the content object.");
                return;
            }

            RectTransform childRect = (RectTransform)content.GetChild(0);
            float childSize = 0f;
            float spacing = 0f;
            float scrollPos = 0f;

            switch (_layoutGroup)
            {
                case HorizontalLayoutGroup horizontalLayoutGroup:
                    childSize = childRect.rect.width;
                    spacing = horizontalLayoutGroup.spacing;
                    scrollPos = index * (childSize + spacing);

                    DOVirtual.Float(content.localPosition.x, -scrollPos, duration, value => content.localPosition = new Vector3(value, content.localPosition.y, content.localPosition.z));
                    break;

                case VerticalLayoutGroup verticalLayoutGroup:
                    childSize = childRect.rect.height;
                    spacing = verticalLayoutGroup.spacing;
                    scrollPos = index * (childSize + spacing);

                    DOVirtual.Float(content.localPosition.y, -scrollPos, duration, value => content.localPosition = new Vector3(content.localPosition.x, value, content.localPosition.z));
                    break;
            }
        }
        
        private void AdjustPadding()
        {
            _layoutGroup ??= content.GetComponent<LayoutGroup>();

            if (_layoutGroup == null)
            {
                Debug.Log("There is no layout group on the content object.");
                return;
            }

            if (content.childCount == 0)
            {
                return;
            }

            RectTransform childRect = (RectTransform)content.GetChild(0);
            float childHalfSize = 0f;

            switch (_layoutGroup)
            {
                case HorizontalLayoutGroup:
                    childHalfSize = childRect.rect.width * 0.5f;
                    _layoutGroup.padding.left = _layoutGroup.padding.right = Mathf.RoundToInt(viewRect.rect.width * 0.5f - childHalfSize);
                    break;
                case VerticalLayoutGroup:
                    childHalfSize = childRect.rect.height * 0.5f;
                    _layoutGroup.padding.top = _layoutGroup.padding.bottom = Mathf.RoundToInt(viewRect.rect.width * 0.5f - childHalfSize);
                    break;
            }
        }

        protected override void OnValidate()
        {
            base.OnValidate();

            AdjustPadding();
        }
    }
}