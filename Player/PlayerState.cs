using System;
using UnityEngine;


namespace Platformer2D.Player
{
    public class PlayerState : MonoBehaviour
    {
        // State
        private bool isGrounded;
        private bool isJumping;
        private bool isReadyToJump;

        // Set events to notify when states change
        public event Action<bool> OnGroundedStateChanged;
        public event Action<bool> OnJumpingStateChanged;
        public event Action<bool> OnReadyToJumpChanged;

        // Open for extension, closed for modification
        public bool IsGrounded
        {
            get { return isGrounded; }
            private set
            {
                if (value != isGrounded)
                {
                    isGrounded = value;
                    OnGroundedStateChanged?.Invoke(isGrounded);
                }
            }
        }

        public bool IsJumping
        {
            get { return isJumping; }
            set
            {
                if (isJumping != value)
                {
                    isJumping = value;
                    OnJumpingStateChanged?.Invoke(isJumping);
                }
            }
        }

        public bool IsReadyToJump
        {
            get { return isReadyToJump; }
            set
            {
                if (isReadyToJump != value)
                {
                    isReadyToJump = value;
                    OnReadyToJumpChanged?.Invoke(isReadyToJump);
                }
            }
        }

        // Public setter for grounded state
        public void SetGroundedState(bool grounded)
        {
            if (CanChangeGroundedState(grounded))
            {
                IsGrounded = grounded;
            }
        }
        public void SetJumpingState(bool jumping)
        {
            if (CanChangeJumpingState(jumping))
            {
                IsJumping = jumping;
            }
        }

        // Check condition for the state change
        private bool CanChangeGroundedState(bool newGroundedState)
        {
            // Aquí puedes agregar cualquier lógica para validar el cambio de estado
            // Por ejemplo, podrías verificar si el jugador está en el aire, haciendo una acción específica, etc.
            // Devuelve 'true' si el cambio es válido, 'false' de lo contrario

            return true; // Cambia esto según tu lógica específica
        }
        private bool CanChangeJumpingState(bool newJumpingState)
        {
            // Aquí puedes agregar cualquier lógica para validar el cambio de estado
            // Por ejemplo, podrías verificar si el jugador está en el aire, haciendo una acción específica, etc.
            // Devuelve 'true' si el cambio es válido, 'false' de lo contrario

            return true; // Cambia esto según tu lógica específica
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                SetGroundedState(true);
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                SetGroundedState(false);
            }
        }
    }
}
