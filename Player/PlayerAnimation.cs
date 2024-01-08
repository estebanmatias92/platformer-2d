using UnityEngine;

namespace Platformer2D.Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        private PlayerMovement playerMovement;
        private Animator animator;

        private void Awake()
        {
            GetPlayerComponents();
        }

        private void GetPlayerComponents()
        {
            if (!TryGetComponent(out playerMovement)) { ShowNullReferenceError(playerMovement); }
            if (!TryGetComponent(out animator)) { ShowNullReferenceError(animator); }

            // Utility method just for this scope
            void ShowNullReferenceError(Component component)
            {
                Debug.LogError("[NULL REFERENCE]: " + component.GetType().ToString() + " can't be found");
            }
        }

        /// <summary>
        /// Takes an Enum and sets animation booleans and floats for blenders
        /// </summary>
        /// <param name="state"></param>
        /// 
        internal void executeAnimation(States state)
        {
            animator.SetBool("isJumping", state == States.Jumping);
            animator.SetFloat("xVelocity", playerMovement.absHorizontalVelocity());
        }
    }
}