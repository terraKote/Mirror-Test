using System;
using UnityEngine;

namespace Project.UserInterface.Widgets
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class BaseWidget : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup _canvasGroup;

        public void SetVisible(bool isVisible)
        {
            _canvasGroup.alpha = isVisible ? 1f : 0f;
            _canvasGroup.blocksRaycasts = isVisible;
        }

        private void OnValidate()
        {
            if (_canvasGroup != null)
                return;

            _canvasGroup = GetComponent<CanvasGroup>();
        }
    }
}