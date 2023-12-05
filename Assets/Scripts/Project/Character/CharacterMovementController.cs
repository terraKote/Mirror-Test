using Mirror;
using UnityEngine;

namespace Project.Character
{
    public class CharacterMovementController : NetworkBehaviour
    {
        [SerializeField]
        private CharacterController _characterController;

        [SerializeField]
        private AnimationController _animationController;

        [SerializeField]
        private float _moveSpeed = 5f;

        private Vector3 _input;
        
        public void SetInput(Vector3 input)
        {
            _input = input;
        }

        private void FixedUpdate()
        {
            if(!isLocalPlayer)
                return;
            
            ApplyCharacterMovement();
        }
        
        private void ApplyCharacterMovement()
        {
            _characterController.Move(_input * (_moveSpeed * Time.deltaTime));
            _animationController.SetMovementVelocity(Mathf.Min(1f, _characterController.velocity.magnitude));

            if (_input.magnitude > 0f)
            {
                transform.rotation = Quaternion.LookRotation(_input);
            }
        }
    }
}