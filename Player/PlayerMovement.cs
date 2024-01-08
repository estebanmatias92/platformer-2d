using System;
using UnityEngine;

namespace Platformer2D.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        // GameObject Components
        private Rigidbody2D rb2d;

        // Editor fields
        [SerializeField] private float moveSpeed = 10f;

        // Events
        public event Action<bool> OnMovingStateChanged;

        // Properties
        private float horizontalInput;
        private float velocityThreshold = 0.01f;
        private bool isMoving;

        public bool IsPlayerMoving
        {
            get { return isMoving; }
            set
            {
                if (value != isMoving)
                {
                    isMoving = value;
                    OnMovingStateChanged?.Invoke(isMoving); // Maybe it's useless
                }
            }
        }

        public float HorizontalInput
        {
            get { return horizontalInput; }
            set { horizontalInput = value; }
        }

        private void Awake()
        {
            rb2d = GetComponent<Rigidbody2D>();
            if (rb2d == null)
                Debug.LogError("Rigidbody2D not found in the object.");
        }

        internal void executeMove()
        {
            rb2d.velocity = new Vector2(horizontalInput * moveSpeed, rb2d.velocity.y);
            IsPlayerMoving = absHorizontalVelocity() > velocityThreshold;
        }

        internal bool isPlayerFacingRight()
        {
            return horizontalInput > velocityThreshold;
        }

        internal bool isPlayerFacingLeft()
        {
            return horizontalInput < -velocityThreshold;
        }

        public float absHorizontalVelocity()
        {
            return Mathf.Abs(rb2d.velocity.x);
        }
    }
}
