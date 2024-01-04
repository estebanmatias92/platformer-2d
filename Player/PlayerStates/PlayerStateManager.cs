using Platformer2D.StateMachine;
using Platformer2D.Utils;
using UnityEngine;

namespace Platformer2D.Player.PlayerStates
{
    public class PlayerStateManager : StateManager<EPlayerState>
    {
        // GameObject components
        private PlayerController playerController;

        // Fields
        [SerializeField] private float jumpBufferTime = 0.2f; // Adjust the buffer time to jump

        // States
        private IdleState idleState;
        private RunningState runningState;
        private ReadyToJumpState readyToJumpState;
        private JumpingState jumpingState;

        private void Awake()
        {
            // Get componentes first
            GetComponents();

            // Populate the dictionary with the states
            AddStates();

            // Set the initial state as current
            currentState = States[EPlayerState.Jumping];
        }

        private void OnEnable()
        {
            // Subscribe States to events from GameObject components
            SubscribeStatesToEvents();
        }

        private void OnDisable()
        {
            UnsubscribeStatesFromEvents();
        }

        /// <summary>
        /// Get components from GameObject
        /// </summary>
        private void GetComponents()
        {
            playerController = GetComponent<PlayerController>();
        }

        /// <summary>
        /// Add each state to the States dictionary
        /// </summary>
        private void AddStates()
        {
            States.Add(EPlayerState.Idle, idleState = new IdleState());
            States.Add(EPlayerState.Running, runningState = new RunningState());
            States.Add(EPlayerState.ReadyToJump, readyToJumpState = new ReadyToJumpState());
            States.Add(EPlayerState.Jumping, jumpingState = new JumpingState(new Timer(jumpBufferTime)));
        }

        /// <summary>
        /// Get states to SUBSCRIBE TO component events
        /// </summary>
        private void SubscribeStatesToEvents()
        {
            playerController.OnPlayerStill += idleState.HandlePlayerStill;
            playerController.OnPlayerStill += runningState.HandlePlayerStill;
            playerController.OnPlayerStill += jumpingState.HandlePlayerStill;

            playerController.OnPlayerJumpInput += idleState.HandlePlayerWantsToJump;
            playerController.OnPlayerJumpInput += runningState.HandlePlayerWantsToJump;
            playerController.OnPlayerJumpInput += jumpingState.HandlePlayerWantsToJump;

            playerController.OnPlayerAirborne += jumpingState.HandlePlayerAirborne;
            //playerController.OnPlayerAirborne += readyToJumpState.HandlePlayerAirborne;
        }

        /// <summary>
        /// Get states to UNSUBSCRIBE FROM component events
        /// </summary>
        private void UnsubscribeStatesFromEvents()
        {
            playerController.OnPlayerStill -= idleState.HandlePlayerStill;
            playerController.OnPlayerStill -= runningState.HandlePlayerStill;
            playerController.OnPlayerStill -= jumpingState.HandlePlayerStill;

            playerController.OnPlayerJumpInput -= idleState.HandlePlayerWantsToJump;
            playerController.OnPlayerJumpInput -= runningState.HandlePlayerWantsToJump;
            playerController.OnPlayerJumpInput -= jumpingState.HandlePlayerWantsToJump;

            playerController.OnPlayerAirborne -= jumpingState.HandlePlayerAirborne;
            //playerController.OnPlayerAirborne += readyToJumpState.HandlePlayerAirborne;
        }
    }
}
