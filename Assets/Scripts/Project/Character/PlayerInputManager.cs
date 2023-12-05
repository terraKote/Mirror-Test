using Mirror;
using UnityEngine;

namespace Project.Character
{
    [RequireComponent(typeof(CharacterMovementController))]
    public class PlayerInputManager : NetworkBehaviour
    {
        [SerializeField]
        private CharacterMovementController _characterMovementController;

        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (!isLocalPlayer)
                return;

            var input = _camera.transform.right * Input.GetAxisRaw("Horizontal") +
                        Vector3.ProjectOnPlane(_camera.transform.forward, Vector3.up).normalized *
                        Input.GetAxisRaw("Vertical");
            input = Vector3.ClampMagnitude(input, 1f);

            _characterMovementController.SetInput(input);
        }
    }
}