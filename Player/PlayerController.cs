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
    [RequireComponent(typeof(PlayerAnimation))]

    public class PlayerController : MonoBehaviour
    {
        // GameObject Components
        private Rigidbody2D rb2d;
        private PlayerJump playerJump;
        private PlayerMovement playerMovement;
        private PlayerCollision playerCollision;
        private PlayerAnimation playerAnimation;

        // Editor fields
        [SerializeField] private float jumpBufferTime = 0.2f; // Adjust the buffer time to jump
        [SerializeField] private float wallJumpCooldown = 1f;


        // Properties
        private Timer jumpBufferTimer;
        private Timer wallJumpTimer;

        private bool isPlayerGrounded;
        private bool isPlayerRunning; // Maybe it's useless
        private bool isPlayerReadyToJump;
        private bool isPlayerReadyToWallJump;
        private bool isPlayerWalled;

        private void Awake()
        {
            // Extract the components needed here
            GetPlayerComponents();

            jumpBufferTimer = new Timer(jumpBufferTime); // Set JumpBuffer initial state
            wallJumpTimer = new Timer(wallJumpCooldown);
        }

        private void Start()
        {
            isPlayerGrounded = false;
            isPlayerReadyToJump = false;
            isPlayerReadyToWallJump = false;
        }

        private void OnEnable()
        {
            playerCollision.OnGroundedStateChanged += HandleOnGroundStateChange;
            playerCollision.OnWalledStateChanged += HandleOnWalledStateChange;
            playerMovement.OnMovingStateChanged += HandleOnMovingStateChange;
        }

        private void OnDisable()
        {
            playerCollision.OnGroundedStateChanged -= HandleOnGroundStateChange;
            playerCollision.OnWalledStateChanged -= HandleOnWalledStateChange;
            playerMovement.OnMovingStateChanged -= HandleOnMovingStateChange;
        }
        private void Update()
        {
            jumpBufferTimer.UpdateTimer(); // Keeps track of the Jump Buffer
            wallJumpTimer.UpdateTimer();

            flipCharacterSprite();
            HandlePlayerAnimation();
        }

        private void FixedUpdate()
        {
            HandlePlayerMove();
            HandleJump();
            handleWallJump();
        }

        // Unity (new) Input System - Unity Events
        public void OnMove(InputAction.CallbackContext inputAction)
        {
            playerMovement.HorizontalInput = inputAction.ReadValue<Vector2>().x;
        }


        // Handles logic when Player wants to jump
        public void OnJump(InputAction.CallbackContext inputAction)
        {
            // Exit if the butten is not pressed
            if (!inputAction.performed) { return; }

            // Set the JUMP READY STATE Or start ticking the jump buffer
            if (isPlayerGrounded) { isPlayerReadyToJump = true; }
            else { jumpBufferTimer.Start(); }
            // If is on the wall and not grounded, can do wall jumps
            if (isPlayerWalled && !isPlayerGrounded) { isPlayerReadyToWallJump = true; }
        }

        /// <summary>
        /// Try and GetComponents for this script, with a nullreference check and Error log.
        /// </summary>
        private void GetPlayerComponents()
        {
            if (!TryGetComponent(out rb2d)) { ShowNullReferenceError(rb2d); }

            if (!TryGetComponent(out playerJump)) { ShowNullReferenceError(playerJump); }
            if (!TryGetComponent(out playerMovement)) { ShowNullReferenceError(playerMovement); }
            if (!TryGetComponent(out playerCollision)) { ShowNullReferenceError(playerCollision); }
            if (!TryGetComponent(out playerAnimation)) { ShowNullReferenceError(playerAnimation); }

            // Utility method just for this scope
            void ShowNullReferenceError(Component component)
            {
                Debug.LogError("[NULL REFERENCE]: " + component.GetType().ToString() + " can't be found");
            }
        }

        /// <summary>
        /// Handler for collision grounded state event from PlayerCollison component.
        /// </summary>
        /// <param name="isGrounded">boolean parameter for the event hook</param>
        private void HandleOnGroundStateChange(bool isGrounded) => isPlayerGrounded = isGrounded;

        private void HandleOnWalledStateChange(bool isWalled) => isPlayerWalled = isWalled;

        /// <summary>
        /// Use the updates from collison and movement to know if the player is running or idlying.
        /// </summary>
        /// <param name="isMoving">Boolean parameter</param>
        private void HandleOnMovingStateChange(bool isMoving) => isPlayerRunning = isMoving && isPlayerGrounded;

        /// <summary>
        /// Sprite render-related.
        /// 
        /// Check horizontal movement if it is facing one direction or another.
        /// Use tranform to change the direction the sprite is facing.
        /// </summary>
        private void flipCharacterSprite()
        {
            if (playerMovement.isPlayerFacingRight())
            {
                transform.localScale = Vector3.one; // Transform to the right
            }
            else if (playerMovement.isPlayerFacingLeft())
            {
                transform.localScale = new Vector3(-1, 1, 1); // Transform to the left
            }
        }

        /// <summary>
        /// Handles the movement for player, 
        /// </summary>
        private void HandlePlayerMove()
        {
            playerMovement.executeMove();

            // Modify this to only stop vertical movement
            if (isPlayerWalled && !isPlayerGrounded)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
            }
        }

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
                jumpBufferTimer.Start();
                isPlayerReadyToJump = false;
            }
        }

        private void handleWallJump()
        {
            // Exit if not on the wa
            if (!isPlayerWalled) { return; }

            // Is able to jump from the wall is there is no cooldown and its sticked to it            
            if (isPlayerReadyToWallJump && !wallJumpTimer.IsActive)
            {
                playerJump.executeWallJump();
                wallJumpTimer.Start();
                isPlayerReadyToWallJump = false;
            }
        }

        /// <summary>
        /// Calls animation from the custom component, using Enum states.
        /// </summary>
        private void HandlePlayerAnimation()
        {
            States currentState;

            if (!isPlayerGrounded)
            {
                currentState = States.Jumping;
            }
            else // Unused below here
            {
                currentState = isPlayerRunning ? States.Running : States.Idle;
            }

            playerAnimation.executeAnimation(currentState);
        }
    }
}