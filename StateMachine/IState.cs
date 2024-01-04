using System;

namespace Platformer2D.StateMachine
{
    public interface IState<EState> where EState : Enum
    {
        void EnterState();
        void ExitState();
        void UpdateState();
        EState GetNextState();
    }
}

