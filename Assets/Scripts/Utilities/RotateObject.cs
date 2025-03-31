using UnityEngine;

namespace Utilities
{
    public class RotateObject : MonoBehaviour
    {
        [SerializeField] private Vector3 _rotationAxis = Vector3.forward;

        [SerializeField] private float _rotationSpeed = 90;

        private void Update()
        {
            transform.Rotate(_rotationAxis, _rotationSpeed * Time.deltaTime);
        }
    }
}