using UnityEngine;
using UnityEngine.UI;

namespace Utilities.UI
{
    public class CenteredScrollRect : ScrollRect
    {
        private LayoutGroup _layoutGroup;

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