using Mirror;
using Project.Network.Messages;
using UnityEngine;

namespace Project.Network
{
    public class GameNetworkManager : NetworkManager
    {
        public override void OnStartServer()
        {
            base.OnStartServer();
            //NetworkServer.RegisterHandler<CreateCharacterMessage>(OnCreateCharacter);
        }

        public override void OnStopServer()
        {
           // NetworkServer.UnregisterHandler<CreateCharacterMessage>();
            base.OnStopServer();
        }

        private void OnCreateCharacter(NetworkConnectionToClient connection, CreateCharacterMessage message)
        {
            var player = Instantiate(playerPrefab);
            Debug.Log($"Index is: {message.colorIndex}");
            NetworkServer.AddPlayerForConnection(connection, player);
        }
    }
}