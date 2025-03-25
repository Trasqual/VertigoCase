using UnityEngine;

namespace Utilities.UI
{
    public class RectTransformAnchorSetter : MonoBehaviour
    {
        [ContextMenu("SetAnchorsToCorners")]
        public void SetAnchorsToCorners()
        {
            RectTransform rt = GetComponent<RectTransform>();

            RectTransform parent = rt.parent as RectTransform;
            if (parent == null) return;

            Vector2 newAnchorMin = new (
                rt.anchorMin.x + rt.offsetMin.x / parent.rect.width,
                rt.anchorMin.y + rt.offsetMin.y / parent.rect.height
            );

            Vector2 newAnchorMax = new (
                rt.anchorMax.x + rt.offsetMax.x / parent.rect.width,
                rt.anchorMax.y + rt.offsetMax.y / parent.rect.height
            );

            rt.anchorMin = newAnchorMin;
            rt.anchorMax = newAnchorMax;

            rt.offsetMin = Vector2.zero;
            rt.offsetMax = Vector2.zero;
        }
    }
}
