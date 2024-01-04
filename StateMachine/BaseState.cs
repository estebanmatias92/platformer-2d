using System;

namespace Platformer2D.StateMachine
{
    public abstract class BaseState<EState> : IState<EState> where EState : Enum
    {
        public EState StateKey { get; private set; }

        protected BaseState(EState key)
        {
            StateKey = key;
        }

        public abstract void EnterState();
        public abstract void ExitState();
        public abstract void UpdateState();
        public abstract EState GetNextState();
    }
}

