using Platformer2D.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Platformer2D.Player
{
    public class PlayerJump : MonoBehaviour
    {
        // Components
        private Rigidbody2D rb2d;
        private PlayerState playerState;

        // Customizable fields
        [SerializeField] private float jumpForce = 16f; // Adjust the jump strengh
        [SerializeField] private float jumpBufferTime = 0.2f; // Adjust the buffer time to jump

        // Properties
        private Timer jumpBufferTimer;

        private void Awake()
        {
            // Get components
            rb2d = GetComponent<Rigidbody2D>();
            playerState = GetComponent<PlayerState>();
            if (rb2d == null)
                Debug.LogError("Rigidbody2D not found in this GameObject.");
            if (playerState == null)
                Debug.LogError("PlayerState not found in this GameObject.");

            // Set JumpBuffer initial state
            jumpBufferTimer = new Timer(jumpBufferTime);

            // Subscribe to these events
            playerState.OnGroundedStateChanged += HandleGroundedStateChange;
            playerState.OnJumpingStateChanged += HandleJumpingStateChange;
        }

        // Triggers when the objects is garbage collected.
        private void OnDestroy()
        {
            // Unsubscribe from these events
            playerState.OnGroundedStateChanged -= HandleGroundedStateChange;
            playerState.OnJumpingStateChanged -= HandleJumpingStateChange;
        }

        private void Update()
        {
            jumpBufferTimer.UpdateTimer(); // Keeps track of the Jump Buffer
        }

        private void FixedUpdate()
        {
            handleJump();
        }

        // Jump execution
        private void handleJump()
        {
            if (playerState.IsReadyToJump)
            {
                rb2d.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                playerState.SetJumpingState(true); // The player is jumping
            }
        }

        // Handles logic when Player wants to jump
        public void OnJump(InputAction.CallbackContext inputAction)
        {
            if (inputAction.performed)
            {
                // Set the JUMP READY STATE to true if the player is on the ground
                if (playerState.IsGrounded)
                {
                    playerState.IsReadyToJump = true;
                }

                // OR start the JUMP BUFFER if the player is mid air
                if (playerState.IsJumping)
                {
                    jumpBufferTimer.Start();
                }
            }
        }

        // Use events from other objects
        private void HandleGroundedStateChange(bool isGrounded)
        {
            if (isGrounded && jumpBufferTimer.IsRunning)
            {
                jumpBufferTimer.Stop();
                playerState.IsReadyToJump = true; // Oh!, Hello trigger state!
            }
        }

        private void HandleJumpingStateChange(bool isJumping)
        {
            playerState.IsReadyToJump = false; // Resets back to false, can't jump mid air
        }

    }
}
