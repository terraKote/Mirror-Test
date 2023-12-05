using System;
using Cinemachine;
using Project.FSM;
using Project.UserInterface.Widgets;
using UnityEngine;

namespace Project.Core.States
{
    public class MainMenuState : State
    {
        private readonly MainMenuWidget _widget;
        private readonly CinemachineVirtualCamera _camera;
        private readonly Action<string> OnModeSelected;

        public MainMenuState(MainMenuWidget widget, CinemachineVirtualCamera camera,
            Action<string> onModeSelectedCallback)
        {
            _widget = widget;
            _camera = camera;
            OnModeSelected = onModeSelectedCallback;
        }

        public override void Enter()
        {
            _widget.OnSinglePlayerModeSelected += OnSinglePlayerModeHandler;
            _widget.OnMultiplayerModeSelected += OnMultiplayerModeHandler;
            _widget.SetVisible(true);

            _camera.Priority = PersistentValues.ACTIVE_CAMERA_PRIORITY;
        }

        public override void Exit()
        {
            _widget.OnSinglePlayerModeSelected -= OnSinglePlayerModeHandler;
            _widget.OnMultiplayerModeSelected -= OnMultiplayerModeHandler;
            _widget.SetVisible(false);

            _camera.Priority = PersistentValues.INACTIVE_CAMERA_PRIORITY;
        }

        private void OnSinglePlayerModeHandler()
        {
            OnModeSelected?.Invoke(nameof(SinglePlayerModeState));
        }

        private void OnMultiplayerModeHandler()
        {
            OnModeSelected?.Invoke(nameof(MultiplayerModeState));
        }
    }
}