using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UISystem.Animations
{
    public class WheelOfFortuneAnimation : MonoBehaviour
    {
        [SerializeField] private WheelOfFortuneSpinAnimationSettings _settings;

        [SerializeField] private int _stopCount = 8;

        private bool _isSpinning;

        public void SetStopCount(int stopCount)
        {
            _stopCount = stopCount;
        }

        public void ApplySettings(WheelOfFortuneSpinAnimationSettings wheelOfFortuneSpinAnimationSettings)
        {
            _settings = wheelOfFortuneSpinAnimationSettings;
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
            float speed = Random.Range(_settings.MaxSpinSpeed * maxSpeedRandomnessRange, _settings.MaxSpinSpeed);

            while (elapsedTime < _settings.SpinDuration)
            {
                float angleIncrement = speed * Time.deltaTime * (_settings.Clockwise ? -1 : 1);
                transform.Rotate(Vector3.forward, angleIncrement);

                const float deceleration = 0.98f;
                speed = Mathf.Max(_settings.MinSpinSpeed, speed * deceleration);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            float stoppedAngle = transform.eulerAngles.z;
            float closestStopAngle = GetClosestStopAngle(stoppedAngle);

            yield return transform.DORotate(new Vector3(0f, 0f, closestStopAngle), _settings.SnapSpeed).SetSpeedBased().WaitForCompletion();

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