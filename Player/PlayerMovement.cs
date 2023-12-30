using UnityEngine;
using UnityEngine.InputSystem;

namespace Platformer2D.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        /// Components
        private Rigidbody2D rb2d;

        // Customizable fields
        [SerializeField] private float moveSpeed = 10f;

        // Properties
        private float horizontalMovement = 0f;

        private void Awake()
        {
            rb2d = GetComponent<Rigidbody2D>();
            if (rb2d == null)
                Debug.LogError("Rigidbody2D not found in the object.");
        }

        private void FixedUpdate()
        {
            handleMove();
        }

        private void handleMove()
        {
            horizontalMovement *= moveSpeed;
            rb2d.velocity = new Vector2(horizontalMovement, rb2d.velocity.y);
        }

        // Unity (new) Input System - Unity Events
        public void OnMove(InputAction.CallbackContext inputAction)
        {
            horizontalMovement = inputAction.ReadValue<Vector2>().x;
        }
    }
}
