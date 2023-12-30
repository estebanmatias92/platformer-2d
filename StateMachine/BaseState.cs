using System;
using UnityEngine;

namespace Platformer2D.StateMachine
{
    public abstract class BaseState<EState> where EState : Enum
    {
        public EState StateKey { get; private set; }

        public BaseState(EState key)
        {
            StateKey = key;
        }

        public abstract void EnterState();
        public abstract void ExitState();
        public abstract void UpdateState();
        public abstract EState GetNextState();
        public abstract void OnTriggerEnter2D(Collider2D other);
        public abstract void OnTriggerStay2D(Collider2D other);
        public abstract void OnTriggerExit2D(Collider2D other);
    }
}
