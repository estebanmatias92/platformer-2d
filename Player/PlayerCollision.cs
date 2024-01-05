using System;
using UnityEngine;

namespace Platformer2D.Player
{
    public class PlayerCollision : MonoBehaviour
    {
        private bool isGrounded; // Flag / real value
        private bool IsPlayerGrounded // write-only and private
        {
            set
            {
                if (value != isGrounded)
                {
                    isGrounded = value;
                    OnGroundedStateChanged?.Invoke(isGrounded);
                }

            }
        }

        // Events
        public event Action<bool> OnGroundedStateChanged;

        // MonoBehaviour event hooks are in sync with the FixedUpdate cycle rate
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                IsPlayerGrounded = true;
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                IsPlayerGrounded = false;

            }
        }
    }
}