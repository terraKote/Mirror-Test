using System;
using Cinemachine;
using Project.Character;
using Project.Core.States;
using Project.FSM;
using Project.UserInterface.Widgets;
using UnityEngine;

namespace Project.Core
{
    public class MainMenuContext : MonoBehaviour
    {
        [Header("Application Context")]
        [SerializeField]
        private ApplicationContext _applicationContext;

        [Header("Default State")]
        [SerializeField]
        private CinemachineVirtualCamera _defaultCamera;

        [SerializeField]
        private MainMenuWidget _mainMenuWidget;

        [Header("Single Player")]
        [SerializeField]
        private CinemachineVirtualCamera _singlePlayerCamera;

        [SerializeField]
        private GameModeWidget _singlePlayerGameModeWidget;

        [Header("Multiplayer")]
        [SerializeField]
        private CinemachineVirtualCamera _multiplayerCamera;

        [SerializeField]
        private GameModeWidget _multiplayerGameModeWidget;

        [SerializeField]
        private CustomizationWidget _customizationWidget;

        [SerializeField]
        private CharacterCustomizationData _characterCustomizationData;

        [SerializeField]
        private AppearanceController _appearanceController;

        [SerializeField]
        private GameObject _playerPrefab;

        private FiniteStateMachine _stateMachine;
        private string _currentStateId;

        private void Awake()
        {
            _singlePlayerGameModeWidget.SetVisible(false);
            _multiplayerGameModeWidget.SetVisible(false);
            _customizationWidget.SetVisible(false);

            var mainMenuState = new MainMenuState(_mainMenuWidget, _defaultCamera, mode => _currentStateId = mode);
            var singlePlayerModeState = new SinglePlayerModeState(_singlePlayerGameModeWidget, _singlePlayerCamera,
                () => _applicationContext.PlaySinglePlayer(),
                () => _currentStateId = nameof(MainMenuState));
            var multiplayerModeState = new MultiplayerModeState(_multiplayerGameModeWidget, _multiplayerCamera,
                _customizationWidget, _characterCustomizationData, _appearanceController, _applicationContext,
                _playerPrefab,
                () => _currentStateId = nameof(MainMenuState));

            var transitions = new Transition[]
            {
                // From menu to other states
                new Transition(() => string.Equals(_currentStateId, nameof(SinglePlayerModeState)),
                    mainMenuState, singlePlayerModeState),
                new Transition(() => string.Equals(_currentStateId, nameof(MultiplayerModeState)),
                    mainMenuState, multiplayerModeState),

                // From other states to menu
                new Transition(() => string.Equals(_currentStateId, nameof(MainMenuState)),
                    singlePlayerModeState, mainMenuState),
                new Transition(() => string.Equals(_currentStateId, nameof(MainMenuState)),
                    multiplayerModeState, mainMenuState),
            };

            _stateMachine = new FiniteStateMachine(transitions, mainMenuState);
        }

        private void Start()
        {
            _stateMachine.Start();
        }

        private void Update()
        {
            _stateMachine.Tick(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            _stateMachine.FixedTick(Time.fixedTime);
        }

        private void OnDestroy()
        {
            _stateMachine.Dispose();
        }
    }
}