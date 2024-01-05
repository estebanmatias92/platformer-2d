using Platformer2D.Utils;
using UnityEngine;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug;

namespace Platformer2D.Player
{
    /// <summary>
    /// This class handles the logic for the Player.
    /// 
    /// Uses other components as input (events about states) 
    /// and outputs (physics execution).
    /// 
    /// And handles the User Input system through event hooks.
    /// </summary>
    [RequireComponent(typeof(PlayerJump))]
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerCollision))]
    public class PlayerController : MonoBehaviour
    {
        // GameObject Components
        private PlayerJump playerJump;
        private PlayerMovement playerMovement;
        private PlayerCollision playerCollision;

        // Editor fields
        [SerializeField] private float jumpBufferTime = 0.2f; // Adjust the buffer time to jump

        // Properties
        private Timer jumpBufferTimer;
        private float horizontalInput;

        private bool isPlayerGrounded;
        private bool isPlayerReadyToJump;

        private void Awake()
        {
            // Extract the components needed here
            GetPlayerComponents();

            jumpBufferTimer = new Timer(jumpBufferTime); // Set JumpBuffer initial state
        }

        private void Start()
        {
            isPlayerGrounded = false;
            isPlayerReadyToJump = false;
        }

        private void OnEnable() => playerCollision.OnGroundedStateChanged += HandleOnGroundStateChange;

        private void OnDisable() => playerCollision.OnGroundedStateChanged -= HandleOnGroundStateChange;

        private void Update()
        {
            jumpBufferTimer.UpdateTimer(); // Keeps track of the Jump Buffer
        }

        private void FixedUpdate()
        {
            playerMovement.executeMove(horizontalInput);
            HandleJump();
        }

        // Unity (new) Input System - Unity Events
        public void OnMove(InputAction.CallbackContext inputAction)
        {
            horizontalInput = inputAction.ReadValue<Vector2>().x;
        }


        // Handles logic when Player wants to jump
        public void OnJump(InputAction.CallbackContext inputAction)
        {
            // Exit if the butten is not pressed
            if (!inputAction.performed) { return; }

            // Set the JUMP READY STATE Or start ticking the jump buffer
            if (isPlayerGrounded) { isPlayerReadyToJump = true; }
            else { jumpBufferTimer.Start(); }
        }

        /// <summary>
        /// Try and GetComponents for this script, with a nullreference check and Error log.
        /// </summary>
        private void GetPlayerComponents()
        {
            if (!TryGetComponent(out playerJump)) { ShowNullReferenceError(playerJump); }
            if (!TryGetComponent(out playerMovement)) { ShowNullReferenceError(playerMovement); }
            if (!TryGetComponent(out playerCollision)) { ShowNullReferenceError(playerCollision); }

            // Utility method just for this scope
            void ShowNullReferenceError(Component component)
            {
                Debug.LogError("[NULL REFERENCE]: " + component.GetType().ToString() + " can't be found");
            }
        }

        /// <summary>
        /// Handler for collision grounded state event from PlayerCollison component
        /// </summary>
        /// <param name="isGrounded">boolean parameter for the event hook</param>
        private void HandleOnGroundStateChange(bool isGrounded) => isPlayerGrounded = isGrounded;


        /// <summary>
        /// Handles the logic for the jump
        /// 
        /// This script has "Jump Buffering" so it handles the buffer timer too.
        /// 
        /// Checks if the Player is on ground, and if it is, 
        /// checks for Player Input or Buffer to execute the jump.
        /// 
        /// Afterwards it resets the flags for jumping.
        /// </summary>
        private void HandleJump()
        {
            // Exit if not on ground
            if (!isPlayerGrounded) { return; }

            if (isPlayerReadyToJump || jumpBufferTimer.IsActive)
            {
                playerJump.executeJump(); // Do the trick Bart!!!

                // Reset timer and flag
                jumpBufferTimer.Stop();
                isPlayerReadyToJump = false;
            }
        }
    }
}