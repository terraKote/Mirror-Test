using System;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UserInterface.Widgets
{
    public class GameModeWidget : BaseWidget
    {
        [SerializeField]
        private Button _beginButton;

        [SerializeField]
        private Button _backButton;

        public event Action OnBeginButtonPress;
        public event Action OnBackButtonPress;

        private void OnEnable()
        {
            _beginButton.onClick.AddListener(OnBeginButtonClickHandler);
            _backButton.onClick.AddListener(OnBackButtonClickHandler);
        }

        private void OnDisable()
        {
            _beginButton.onClick.RemoveListener(OnBeginButtonClickHandler);
            _backButton.onClick.RemoveListener(OnBackButtonClickHandler);
        }

        private void OnBeginButtonClickHandler()
        {
            OnBeginButtonPress?.Invoke();
        }

        private void OnBackButtonClickHandler()
        {
            OnBackButtonPress?.Invoke();
        }
    }
}