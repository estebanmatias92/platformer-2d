using Platformer2D.StateMachine;

namespace Platformer2D.Player.PlayerStates
{
    public class PlayerStateManager : StateManager<EPlayerState>
    {
        private void Awake()
        {
            States.Add(EPlayerState.Idle, new IdleState());
            States.Add(EPlayerState.Running, new RunningState());
            States.Add(EPlayerState.Jumping, new JumpingState());

            currentState = States[EPlayerState.Idle];
        }
    }
}
