using System;
using Cinemachine;
using Project.FSM;
using Project.UserInterface.Widgets;

namespace Project.Core.States
{
    public class SinglePlayerModeState : State
    {
        private readonly GameModeWidget _widget;
        private readonly CinemachineVirtualCamera _camera;
        private readonly Action _onBeginButtonPress;
        private readonly Action _onBackButtonPress;

        public SinglePlayerModeState(GameModeWidget widget, CinemachineVirtualCamera camera, Action onBeginButtonPress,
            Action onBackButtonPressCallback)
        {
            _widget = widget;
            _camera = camera;
            _onBeginButtonPress = onBeginButtonPress;
            _onBackButtonPress = onBackButtonPressCallback;
        }

        public override void Enter()
        {
            _widget.OnBeginButtonPress += OnBeginButtonPressHandler;
            _widget.OnBackButtonPress += OnBackButtonPressHandler;
            _widget.SetVisible(true);

            _camera.Priority = PersistentValues.ACTIVE_CAMERA_PRIORITY;
        }

        public override void Exit()
        {
            _widget.OnBeginButtonPress -= OnBeginButtonPressHandler;
            _widget.OnBackButtonPress -= OnBackButtonPressHandler;
            _widget.SetVisible(false);

            _camera.Priority = PersistentValues.INACTIVE_CAMERA_PRIORITY;
        }

        private void OnBeginButtonPressHandler()
        {
            _onBeginButtonPress?.Invoke();
        }

        private void OnBackButtonPressHandler()
        {
            _onBackButtonPress?.Invoke();
        }
    }
}