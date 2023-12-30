using Platformer2D.StateMachine;
using UnityEngine;

namespace Platformer2D.Player.PlayerStates
{
    // Alias
    internal class JumpingState : BaseState<EPlayerState>
    {
        public JumpingState() : base(EPlayerState.Jumping)
        {

        }

        public override void EnterState()
        {
            Debug.Log("Empeze a correr!");
        }

        public override void ExitState()
        {
            Debug.Log("Termine de correr!");
        }

        public override void UpdateState()
        {
            // Lógica de actualización para Jumping.
        }

        public override EPlayerState GetNextState()
        {
            // Determinar el próximo estado.
            return StateKey; // Permanecer en Jumping por defecto.
        }

        public override void OnTriggerEnter2D(Collider2D other)
        {
            throw new System.NotImplementedException();
        }

        public override void OnTriggerStay2D(Collider2D other)
        {
            throw new System.NotImplementedException();
        }

        public override void OnTriggerExit2D(Collider2D other)
        {
            throw new System.NotImplementedException();
        }
    }
}