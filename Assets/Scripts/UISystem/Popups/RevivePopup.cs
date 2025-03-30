using System;
using ServiceLocatorSystem;
using TMPro;
using UISystem.Core;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace UISystem.Popups
{
    public class RevivePopup : UIPanelBase
    {
        [SerializeField] private Button _reviveButton;
        [SerializeField] private Button _cancelButton;

        [SerializeField] private TMP_Text _reviveButtonText;
        [SerializeField] private TMP_Text _cancelButtonText;
        [SerializeField] private TMP_Text _descriptionText;
        [SerializeField] private TMP_Text _countdownText;

        [SerializeField] private Image _iconImage;

        private UIManager _uiManager;

        private CountdownTimer _countdownTimer;

        private Action _onRevive;
        private Action _onCancel;

        public override string GetPanelID() => UIIDs.AreYouSurePopup;

        private void Update()
        {
            if (!_countdownTimer.IsRunning)
            {
                return;
            }

            _countdownTimer.Tick(Time.deltaTime);
            _countdownText.SetText(Mathf.CeilToInt(_countdownTimer.Progress).ToString());
        }

        public override void ApplyData(object data)
        {
            if (data is not RevivePopupData revivePopupData)
            {
                Debug.Log($"Wrong data {data.GetType()} sent for UI {GetPanelID()}");
                return;
            }

            _uiManager = ServiceLocator.Instance.Get<UIManager>();

            _reviveButtonText.SetText(revivePopupData.ReviveButtonText);
            _cancelButtonText.SetText(revivePopupData.CancelButtonText);

            _onRevive = revivePopupData.OnRevive;
            _onCancel = revivePopupData.OnCancel;

            _descriptionText.SetText(revivePopupData.Description);

            if (revivePopupData.Icon != null)
            {
                _iconImage.gameObject.SetActive(true);
                _iconImage.sprite = revivePopupData.Icon;
            }
            else
            {
                _iconImage.gameObject.SetActive(false);
            }
        }

        public override void Show()
        {
            _reviveButton.onClick.AddListener(OnConfirm);
            _cancelButton.onClick.AddListener(OnCancel);

            _countdownTimer = new CountdownTimer(3f);
            _countdownTimer.OnTimerStop += OnCountdownTimerStopped;
            _countdownTimer.Start();
        }

        public override void Hide()
        {
            ResetSelf();

            _uiManager.ClosePanel(GetPanelID());
        }

        private void ResetSelf()
        {
            _reviveButton.onClick.RemoveAllListeners();
            _cancelButton.onClick.RemoveAllListeners();

            _onRevive = null;
            _onCancel = null;

            _descriptionText.SetText("Are you sure?");
            _reviveButtonText.SetText("Confirm");
            _cancelButtonText.SetText("Cancel");
        }

        public void PauseCountdown()
        {
            _countdownTimer.Pause();
        }

        public void ResumeCountdown()
        {
            _countdownTimer.Resume();
        }

        private void OnCountdownTimerStopped()
        {
            _countdownTimer.OnTimerStop -= OnCountdownTimerStopped;

            _countdownText.gameObject.SetActive(false);
            _reviveButton.gameObject.SetActive(false);
        }

        private void OnConfirm()
        {
            _onRevive?.Invoke();
        }

        private void OnCancel()
        {
            _onCancel?.Invoke();
        }
    }

    public partial class UIIDs
    {
        public static string RevivePopup = "RevivePopup";
    }

    public struct RevivePopupData
    {
        public string Description;
        public string ReviveButtonText;
        public string CancelButtonText;
        public Sprite Icon;
        public Action OnRevive;
        public Action OnCancel;

        public RevivePopupData(string description = "Are you sure?",
                               string reviveButtonText = "Confirm",
                               string cancelButtonText = "Cancel",
                               Sprite icon = null,
                               Action onRevive = null,
                               Action onCancel = null)
        {
            Description = description;
            ReviveButtonText = reviveButtonText;
            CancelButtonText = cancelButtonText;
            Icon = icon;
            OnRevive = onRevive;
            OnCancel = onCancel;
        }
    }
}