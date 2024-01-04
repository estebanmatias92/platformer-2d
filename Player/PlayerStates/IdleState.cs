using Platformer2D.StateMachine;

namespace Platformer2D.Player.PlayerStates
{
    // Alias
    internal class IdleState : BaseState<EPlayerState>
    {
        // We need flags to know when and which state this has to change
        private bool isPlayerReadyToJump;
        private bool isPlayerRunning;

        public IdleState() : base(EPlayerState.Idle) { }

        public override void EnterState()
        {
            isPlayerReadyToJump = false;
            isPlayerRunning = false;
        }

        public override void UpdateState()
        {
            //Debug.Log("Idle");
        }

        public override void ExitState() { }

        public override EPlayerState GetNextState()
        {
            if (isPlayerReadyToJump) { return EPlayerState.ReadyToJump; }

            if (isPlayerRunning) { return EPlayerState.Running; }

            return StateKey; // Keep on Idle state by default.
        }

        // Catch events from GameObject component
        public void HandlePlayerWantsToJump(bool isButtonPressed)
        {
            isPlayerReadyToJump = isButtonPressed;
        }

        public void HandlePlayerStill(bool isStill)
        {
            isPlayerRunning = !isStill;
        }
    }
}