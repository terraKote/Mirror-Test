using System;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UserInterface.Widgets
{
    public class CustomizationWidget : BaseWidget
    {
        [SerializeField]
        private Button _left;

        [SerializeField]
        private Button _right;

        private int _currentSkinIndex;
        private int _skinCount;

        public event Action<int> OnSkinIndexChange;

        private void OnEnable()
        {
            _left.onClick.AddListener(OnLeftClickHandler);
            _right.onClick.AddListener(OnRightClickHandler);
        }

        private void OnDisable()
        {
            _left.onClick.AddListener(OnLeftClickHandler);
            _right.onClick.AddListener(OnRightClickHandler);
        }

        private void OnLeftClickHandler()
        {
            _currentSkinIndex = Mathf.Clamp(_currentSkinIndex - 1, 0, _skinCount);
            OnSkinIndexChange?.Invoke(_currentSkinIndex);
        }

        private void OnRightClickHandler()
        {
            _currentSkinIndex = Mathf.Clamp(_currentSkinIndex + 1, 0, _skinCount);
            OnSkinIndexChange?.Invoke(_currentSkinIndex);
        }

        public void SetSkinCount(int count)
        {
            _skinCount = count;
        }
    }
}