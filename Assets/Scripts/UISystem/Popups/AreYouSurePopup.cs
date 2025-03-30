using System;
using ServiceLocatorSystem;
using TMPro;
using UISystem.Core;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.Popups
{
    public class AreYouSurePopup : UIPanelBase
    {
        [SerializeField] private Button _confirmButton;
        [SerializeField] private Button _cancelButton;

        [SerializeField] private TMP_Text _confirmButtonText;
        [SerializeField] private TMP_Text _cancelButtonText;
        [SerializeField] private TMP_Text _descriptionText;

        private Action _onConfirm;
        private Action _onCancel;

        public override string GetPanelID() => UIIDs.AreYouSurePopup;

        public override void ApplyData(object data)
        {
            if (data is not AreYouSurePopupData areYouSurePopupData)
            {
                Debug.Log($"Wrong data {data.GetType()} sent for UI {GetPanelID()}");
                return;
            }

            _confirmButtonText.SetText(areYouSurePopupData.ConfirmButtonText);
            _cancelButtonText.SetText(areYouSurePopupData.CancelButtonText);

            _onConfirm = areYouSurePopupData.OnConfirm;
            _onCancel = areYouSurePopupData.OnCancel;

            _descriptionText.SetText(areYouSurePopupData.Description);
        }

        public override void Show()
        {
            _confirmButton.onClick.AddListener(OnConfirm);
            _cancelButton.onClick.AddListener(OnCancel);
        }

        public override void Hide()
        {
            ResetSelf();
        }

        private void ResetSelf()
        {
            _confirmButton.onClick.RemoveAllListeners();
            _cancelButton.onClick.RemoveAllListeners();

            _onConfirm = null;
            _onCancel = null;

            _descriptionText.SetText("Are you sure?");
            _confirmButtonText.SetText("Confirm");
            _cancelButtonText.SetText("Cancel");
        }

        private void OnConfirm()
        {
            _onConfirm?.Invoke();
        }

        private void OnCancel()
        {
            _onCancel?.Invoke();
        }
    }

    public partial class UIIDs
    {
        public static string AreYouSurePopup = "AreYouSurePopup";
    }

    public struct AreYouSurePopupData
    {
        public string Description;
        public string ConfirmButtonText;
        public string CancelButtonText;
        public Action OnConfirm;
        public Action OnCancel;

        public AreYouSurePopupData(string description = "Are you sure?",
                                   string confirmButtonText = "Confirm",
                                   string cancelButtonText = "Cancel",
                                   Action onConfirm = null,
                                   Action onCancel = null)
        {
            Description = description;
            ConfirmButtonText = confirmButtonText;
            CancelButtonText = cancelButtonText;
            OnConfirm = onConfirm;
            OnCancel = onCancel;
        }
    }
}