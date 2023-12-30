using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D.StateMachine
{
    public abstract class StateManager<EState> : MonoBehaviour where EState : Enum
    {
        // Collection of states
        protected Dictionary<EState, BaseState<EState>> States = new Dictionary<EState, BaseState<EState>>();
        protected BaseState<EState> currentState;

        protected bool IsTransitioningState = false;

        private void Start()
        {
            currentState.EnterState();
        }

        /// <summary>
        /// Get the next State key, if it is the same, keep updating the state,
        /// otherwise transition to the next state.
        /// </summary>
        private void Update()
        {
            if (IsTransitioningState) { return; }

            EState nextStateKey = currentState.GetNextState();

            if (nextStateKey.Equals(currentState.StateKey))
            {
                currentState.UpdateState();
            }
            else
            {
                TransitionToState(nextStateKey);
            }
        }

        /// <summary>
        /// Handle Exit and Enter method for previous and next states repectively.
        /// Sets a flag to about being referenced from update while is executing.
        /// </summary>
        /// <param name="stateKey">Enum State key</param>
        public void TransitionToState(EState stateKey)
        {
            IsTransitioningState = true;

            currentState.ExitState();
            currentState = States[stateKey];
            currentState.EnterState();

            IsTransitioningState = false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            currentState.OnTriggerEnter2D(other);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            currentState.OnTriggerStay2D(other);

        }

        private void OnTriggerExit2D(Collider2D other)
        {
            currentState.OnTriggerExit2D(other);

        }
    }
}