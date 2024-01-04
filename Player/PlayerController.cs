using Platformer2D.Player.PlayerStates;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Platformer2D.Player
{
    public class PlayerController : MonoBehaviour
    {
        // Components
        private Collision collisionComponent;
        private Movement movementComponent;
        private Jump jumpComponent;
        private PlayerStateManager stateManager;

        // Events
        internal event Action<bool> OnPlayerAirborne;
        internal event Action<bool> OnPlayerStill;
        internal event Action<bool> OnPlayerJumpInput;

        // Properties
        private float horizontalInput = 0f;
        private bool isAbleToJump;

        private void Awake()
        {
            collisionComponent = GetComponent<Collision>();
            movementComponent = GetComponent<Movement>();
            jumpComponent = GetComponent<Jump>();
            stateManager = GetComponent<PlayerStateManager>();
        }

        private void Start()
        {
            isAbleToJump = false;
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void FixedUpdate()
        {
            movementComponent.executeMove(horizontalInput);
            // Flag to trigger Jumping action
            isAbleToJump = stateManager.currentState.StateKey == EPlayerState.ReadyToJump;

            if (collisionComponent.IsPlayerOnGround)
            {
                OnPlayerStill?.Invoke(movementComponent.IsPlayerStill);
                OnPlayerAirborne?.Invoke(false); // Idle or Running depends from being on the ground
            }
            else
            {
                OnPlayerAirborne?.Invoke(true);
            }

            if (isAbleToJump)
            {
                jumpComponent.executeJump();
            }
        }

        // Unity (new) Input System - Unity Events
        public void OnMove(InputAction.CallbackContext inputAction)
        {
            horizontalInput = inputAction.ReadValue<Vector2>().x;
        }

        // Handles logic when Player wants to jump
        public void OnJump(InputAction.CallbackContext inputAction)
        {
            if (inputAction.performed)
            {
                OnPlayerJumpInput?.Invoke(true);
            }
        }
    }
}