using Platformer2D.StateMachine;
using Platformer2D.Utils;
using UnityEngine;

namespace Platformer2D.Player.PlayerStates
{
    internal class JumpingState : BaseState<EPlayerState>
    {
        private Timer jumpBufferTimer; // External tool
        private bool isPlayerReadyToJump;
        private bool isPlayerStill;
        private bool isPlayerAirborne;

        public JumpingState(Timer jumpBufferTimer) : base(EPlayerState.Jumping)
        {
            this.jumpBufferTimer = jumpBufferTimer;
        }

        public override void EnterState()
        {
            jumpBufferTimer.Reset();
            isPlayerReadyToJump = false;
            isPlayerStill = false;
            isPlayerAirborne = true;
        }

        public override void UpdateState()
        {
            Debug.Log("Jumping");
            jumpBufferTimer.UpdateTimer(); // Keeps track of the Jump Buffer

            if (jumpBufferTimer.IsActive && !isPlayerAirborne)
            {
                isPlayerReadyToJump = true;
            }
        }

        public override void ExitState()
        {
            jumpBufferTimer.Stop();
        }

        public override EPlayerState GetNextState()
        {
            if (isPlayerReadyToJump) { return EPlayerState.ReadyToJump; }
            if (!isPlayerStill) { return EPlayerState.Running; }
            if (isPlayerStill) { return EPlayerState.Idle; }

            return StateKey;
        }

        public void HandlePlayerWantsToJump(bool isButtonPressed)
        {
            if (isButtonPressed)
            {
                jumpBufferTimer.Start();
            }
        }

        public void HandlePlayerAirborne(bool isAirborne)
        {
            isPlayerAirborne = isAirborne;
        }

        public void HandlePlayerStill(bool isStill)
        {
            isPlayerStill = isStill;
        }
    }
}