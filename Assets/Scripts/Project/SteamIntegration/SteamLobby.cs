using Mirror;
using Project.Network;
using Steamworks;
using UnityEngine;

namespace Project.SteamIntegration
{
    public class SteamLobby : MonoBehaviour
    {
        private const string HostAddressKey = "HostAddress";

        [SerializeField]
        private GameNetworkManager _manager;

        private Callback<LobbyCreated_t> _lobbyCreated;
        private Callback<GameLobbyJoinRequested_t> _joinRequest;
        private Callback<LobbyEnter_t> _lobbyEnter;

        private ulong _lobbyId;

        private void Start()
        {
            if (!SteamManager.Initialized)
                return;

            _lobbyCreated = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
            _joinRequest = Callback<GameLobbyJoinRequested_t>.Create(OnJoinRequest);
            _lobbyEnter = Callback<LobbyEnter_t>.Create(OnLobbyEntered);
        }

        public void HostLobby()
        {
            SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, _manager.maxConnections);
        }

        private void OnLobbyCreated(LobbyCreated_t callback)
        {
            if (callback.m_eResult != EResult.k_EResultOK)
                return;

            _manager.StartHost();
            var lobby = new CSteamID(callback.m_ulSteamIDLobby);
            SteamMatchmaking.SetLobbyData(lobby, HostAddressKey,
                SteamUser.GetSteamID().ToString());
            SteamMatchmaking.SetLobbyData(lobby, "name", $"{SteamFriends.GetPersonaName()}'s Lobby");
        }

        private void OnJoinRequest(GameLobbyJoinRequested_t callback)
        {
            SteamMatchmaking.JoinLobby(callback.m_steamIDLobby);
        }

        private void OnLobbyEntered(LobbyEnter_t callback)
        {
            var lobby = new CSteamID(callback.m_ulSteamIDLobby);

            // Everyone
            _lobbyId = callback.m_ulSteamIDLobby;

            // Client
            if (NetworkServer.active)
                return;

            _manager.networkAddress = SteamMatchmaking.GetLobbyData(lobby, HostAddressKey);
            _manager.StartClient();
        }
    }
}