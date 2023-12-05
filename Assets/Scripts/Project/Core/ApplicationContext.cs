using Mirror;
using UnityEngine;

namespace Project.Core
{
    public class ApplicationContext : MonoBehaviour
    {
        [SerializeField]
        private NetworkManager _networkManager;
        
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void PlaySinglePlayer()
        {
            _networkManager.StartHost();
        }

        public void StartHost()
        {
            _networkManager.StartHost();
        }
    }
}