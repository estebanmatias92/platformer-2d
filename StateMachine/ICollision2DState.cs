using System;
using UnityEngine;

namespace Platformer2D.StateMachine
{
    public interface ICollision2DState<EState> : IState<EState> where EState : Enum
    {
        public void OnCollisionEnter2D(Collision2D collision) { }
        public void OnCollisionStay2D(Collision2D collision) { }
        public void OnCollisionExit2D(Collision2D collision) { }
    }
}
