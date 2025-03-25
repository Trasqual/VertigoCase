using UnityEngine;
using UnityEngine.UI;

namespace Utilities.UI
{
    public class RadialLayoutGroup : LayoutGroup
    {
        [field: SerializeField] public float RadiusToSizeRatio { get; private set; } = 100f;
        [field: SerializeField] public float StartAngle { get; private set; } = 0f;
        
        [field: SerializeField] public bool Clockwise = true;
        
        private float _angleIncrement = 0f;
        private float _radius;

        public override void CalculateLayoutInputHorizontal() {}
        public override void CalculateLayoutInputVertical() {}
        public override void SetLayoutHorizontal() {}
        public override void SetLayoutVertical() => ArrangeChildren();
        
        private void ArrangeChildren()
        {
            if (transform.childCount == 0) return;
            
            RectTransform rt = GetComponent<RectTransform>();
            _radius = rt.rect.width * RadiusToSizeRatio;

            int childCount = transform.childCount;
            
            _angleIncrement = 360f / childCount;
            
            float angleOffset = Clockwise ? -_angleIncrement : _angleIncrement;
            float angle = StartAngle;

            for (int i = 0; i < childCount; i++)
            {
                RectTransform child = transform.GetChild(i) as RectTransform;
                if (child == null || !child.gameObject.activeSelf) continue;

                float rad = angle * Mathf.Deg2Rad;
                Vector3 position = new (Mathf.Cos(rad) * _radius, Mathf.Sin(rad) * _radius, 0f);
                child.localPosition = position;
                angle += angleOffset;
            }
        }

        protected override void OnValidate()
        {
            ArrangeChildren();
        }
    }
}