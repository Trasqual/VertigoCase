using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UISystem.Animations
{
    public class WheelOfFortuneAnimation : MonoBehaviour
    {
        [SerializeField] private int _stopCount = 8;
        [SerializeField] private float _spinDuration = 2f;
        [SerializeField] private float _minSpinSpeed = 90f;
        [SerializeField] private float _maxSpinSpeed = 1440f;
        [SerializeField] private float _snapSpeed = 50f;

        private bool _isSpinning;
        
        public void SetStopCount(int stopCount)
        {
            _stopCount = stopCount;
        }

        public void Play(Action onComplete = null)
        {
            if (_isSpinning)
            {
                return;
            }

            StartCoroutine(SpinCoroutine(onComplete));
        }

        private IEnumerator SpinCoroutine(Action onComplete)
        {
            _isSpinning = true;

            float elapsedTime = 0f;
            const float maxSpeedRandomnessRange = 0.8f;
            float speed = Random.Range(_maxSpinSpeed * maxSpeedRandomnessRange, _maxSpinSpeed);

            while (elapsedTime < _spinDuration)
            {
                float angleIncrement = speed * Time.deltaTime;
                transform.Rotate(Vector3.forward, angleIncrement);

                const float deceleration = 0.98f;
                speed = Mathf.Max(_minSpinSpeed, speed * deceleration);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            float stoppedAngle = transform.eulerAngles.z;
            float closestStopAngle = GetClosestStopAngle(stoppedAngle);

            yield return transform.DORotate(new Vector3(0f, 0f, closestStopAngle), _snapSpeed).SetSpeedBased().WaitForCompletion();

            _isSpinning = false;

            onComplete?.Invoke();
        }

        private float GetClosestStopAngle(float angle)
        {
            float angleStep = 360f / _stopCount;
            float closestAngle = Mathf.Round(angle / angleStep) * angleStep;
            return closestAngle;
        }

        public int GetClosestStopIndex()
        {
            float angleStep = 360f / _stopCount;
            int closestStop = Mathf.RoundToInt(transform.eulerAngles.z / angleStep);
            return closestStop;
        }
    }
}