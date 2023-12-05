using System;
using Cinemachine;
using Mirror;
using Project.Character;
using Project.FSM;
using Project.UserInterface.Widgets;
using UnityEngine;

namespace Project.Core.States
{
    public class MultiplayerModeState : State
    {
        private readonly GameModeWidget _widget;
        private readonly CinemachineVirtualCamera _camera;
        private readonly CustomizationWidget _customizationWidget;
        private readonly CharacterCustomizationData _characterCustomizationData;
        private readonly AppearanceController _appearanceController;
        private readonly ApplicationContext _applicationContext;
        private readonly GameObject _playerPrefab;
        private readonly Action _onBackButtonPress;

        private int _selectedCharacterTextureIndex;

        public MultiplayerModeState(GameModeWidget widget, CinemachineVirtualCamera camera,
            CustomizationWidget customizationWidget, CharacterCustomizationData characterCustomizationData,
            AppearanceController appearanceController, ApplicationContext applicationContext, GameObject playerPrefab,
            Action onBackButtonPressCallback)
        {
            _widget = widget;
            _camera = camera;
            _customizationWidget = customizationWidget;
            _characterCustomizationData = characterCustomizationData;
            _appearanceController = appearanceController;
            _applicationContext = applicationContext;
            _playerPrefab = playerPrefab;
            _onBackButtonPress = onBackButtonPressCallback;
        }

        public override void Enter()
        {
            _widget.OnBeginButtonPress += OnBeginButtonPressHandler;
            _widget.OnBackButtonPress += OnBackButtonPressHandler;
            _widget.SetVisible(true);

            _customizationWidget.SetSkinCount(_characterCustomizationData.Textures.Length - 1);
            _customizationWidget.OnSkinIndexChange += OnCharacterSkinIndexChange;
            _customizationWidget.SetVisible(true);

            _camera.Priority = PersistentValues.ACTIVE_CAMERA_PRIORITY;
        }

        public override void Exit()
        {
            _widget.OnBeginButtonPress -= OnBeginButtonPressHandler;
            _widget.OnBackButtonPress -= OnBackButtonPressHandler;
            _widget.SetVisible(false);

            _customizationWidget.OnSkinIndexChange -= OnCharacterSkinIndexChange;
            _customizationWidget.SetVisible(false);

            _camera.Priority = PersistentValues.INACTIVE_CAMERA_PRIORITY;
        }

        private void OnBeginButtonPressHandler()
        {
            _applicationContext.StartHost();
            //
            // var message = new CreateCharacterMessage
            // {
            //     colorIndex = _selectedCharacterTextureIndex
            // };
            //
            // NetworkClient.Send(message);
            CreatePlayer(_selectedCharacterTextureIndex);
        }

        [Command]
        private void CreatePlayer(int index, NetworkConnectionToClient sender = null)
        {
            var instance = UnityEngine.Object.Instantiate(_playerPrefab);
            var appearance = instance.GetComponent<AppearanceController>();
            appearance.SetTexture(_characterCustomizationData.Textures[index]);
            NetworkServer.Spawn(instance.gameObject, sender);
            //NetworkServer.AddPlayerForConnection(sender, instance);
        }

        private void OnBackButtonPressHandler()
        {
            _onBackButtonPress?.Invoke();
        }

        private void OnCharacterSkinIndexChange(int index)
        {
            var texture = _characterCustomizationData.Textures[index];
            _appearanceController.SetTexture(texture);

            _selectedCharacterTextureIndex = index;
        }
    }
}