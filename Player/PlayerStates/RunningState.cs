using Platformer2D.StateMachine;

namespace Platformer2D.Player.PlayerStates
{
    // Alias
    internal class RunningState : BaseState<EPlayerState>
    {
        // We need flags to know when and which state this has to change
        private bool isPlayerReadyToJump;
        private bool isPlayerStill;

        public RunningState() : base(EPlayerState.Running) { }

        public override void EnterState()
        {
            isPlayerReadyToJump = false;
            isPlayerStill = true;
        }

        public override void UpdateState()
        {
            //Debug.Log("Running");
        }

        public override void ExitState() { }

        public override EPlayerState GetNextState()
        {
            if (isPlayerReadyToJump) { return EPlayerState.ReadyToJump; }

            if (isPlayerStill) { return EPlayerState.Idle; }

            return StateKey; // Keep on Running state by default.
        }

        public void HandlePlayerWantsToJump(bool isButtonPressed)
        {
            if (isButtonPressed)
            {
                isPlayerReadyToJump = true;
            }
        }

        public void HandlePlayerStill(bool isStill)
        {
            isPlayerStill = isStill;
        }
    }
}