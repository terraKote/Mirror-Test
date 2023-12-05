using Cinemachine;
using Mirror;
using UnityEngine;

namespace Project.Character
{
    public class CameraManager : NetworkBehaviour
    {
        [SerializeField]
        private CinemachineVirtualCamera _camera;

        private void Start()
        {
            _camera.gameObject.SetActive(isLocalPlayer);
        }
    }
}