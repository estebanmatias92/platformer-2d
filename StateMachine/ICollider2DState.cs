using System;
using UnityEngine;

namespace Platformer2D.StateMachine
{
    public interface ICollider2DState<EState> : IState<EState> where EState : Enum
    {
        public void OnTriggerEnter2D(Collider2D other) { }
        public void OnTriggerStay2D(Collider2D other) { }
        public void OnTriggerExit2D(Collider2D other) { }
    }
}

