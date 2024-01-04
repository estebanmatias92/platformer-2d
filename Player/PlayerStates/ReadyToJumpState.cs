using Platformer2D.StateMachine;
using UnityEngine;

namespace Platformer2D.Player.PlayerStates
{
    internal class ReadyToJumpState : BaseState<EPlayerState>
    {
        private bool isPlayerAirbonrne;

        EPlayerState nextState;

        public ReadyToJumpState() : base(EPlayerState.ReadyToJump) { }

        public override void EnterState()
        {
            isPlayerAirbonrne = false;
        }

        public override void UpdateState()
        {
            if (isPlayerAirbonrne)
            {
                nextState = EPlayerState.Jumping;
            }
            else
            {
                nextState = StateKey;
            }
            Debug.Log("Ready to Jump");
        }

        public override void ExitState() { }

        public override EPlayerState GetNextState()
        {
            return nextState;
        }

        public void HandlePlayerAirborne(bool isAirborne)
        {
            if (isAirborne)
            {
                isPlayerAirbonrne = isAirborne;
            }
        }
    }
}
