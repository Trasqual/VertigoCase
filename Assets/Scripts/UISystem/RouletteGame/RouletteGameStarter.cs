using UISystem.RouletteGame.Core;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.RouletteGame
{
    public class RouletteGameStarter : MonoBehaviour
    {
        [SerializeField] private RouletteGameController _rouletteGameController;

        private Button _button;

        private void Awake()
        {
            _button = GetComponentInChildren<Button>();
            _button.onClick.AddListener(OnClick);

            _rouletteGameController.OnExitGame += OnRouletteGameOver;
        }

        private void OnRouletteGameOver()
        {
            _button.interactable = true;
        }

        private void OnClick()
        {
            _rouletteGameController.gameObject.SetActive(true);
            _rouletteGameController.Initialize();
            _button.interactable = false;
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}