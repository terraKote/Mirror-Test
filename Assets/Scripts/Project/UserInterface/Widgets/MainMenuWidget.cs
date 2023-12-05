using System;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UserInterface.Widgets
{
    public class MainMenuWidget : BaseWidget
    {
        [SerializeField]
        private Button _singlePlayerModeButton;

        [SerializeField]
        private Button _multiplayerModeButton;

        public event Action OnSinglePlayerModeSelected;
        public event Action OnMultiplayerModeSelected;

        private void OnEnable()
        {
            _singlePlayerModeButton.onClick.AddListener(OnSinglePlayerModeClickHandler);
            _multiplayerModeButton.onClick.AddListener(OnMultiplayerModeClickHandler);
        }

        private void OnDisable()
        {
            _singlePlayerModeButton.onClick.RemoveListener(OnSinglePlayerModeClickHandler);
            _multiplayerModeButton.onClick.RemoveListener(OnMultiplayerModeClickHandler);
        }

        private void OnSinglePlayerModeClickHandler()
        {
            OnSinglePlayerModeSelected?.Invoke();
        }

        private void OnMultiplayerModeClickHandler()
        {
            OnMultiplayerModeSelected?.Invoke();
        }
    }
}