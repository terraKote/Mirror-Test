using Mirror;
using UnityEngine;

namespace Project.Character
{
    public class AnimationController : NetworkBehaviour
    {
        private static readonly int ForwardProperty = Animator.StringToHash("Forward");

        [SerializeField]
        private Animator _animator;

        public void SetMovementVelocity(float velocity)
        {
            if (!isLocalPlayer)
                return;

            SetAnimatorVelocity(velocity);
        }

        private void SetAnimatorVelocity(float velocity)
        {
            _animator.SetFloat(ForwardProperty, velocity);
        }
    }
}