using UnityEngine;

namespace Platformer2D.Player
{
    public class PlayerJump : MonoBehaviour
    {
        // Components
        private Rigidbody2D rb2d;

        // Customizable fields
        [SerializeField] private float jumpForce = 16f; // Adjust the jump strengh
        [SerializeField] private Vector2 wallJumpForce = new Vector2(10f, 16f);

        private void Awake()
        {
            rb2d = GetComponent<Rigidbody2D>();
            if (rb2d == null)
                Debug.LogError("Rigidbody2D not found in this GameObject.");
        }

        internal void executeJump()
        {
            rb2d.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }

        internal void executeWallJump()
        {
            // Determine the correct xDirection
            float xDirection = -transform.localScale.x;
            float xVelocity = xDirection * wallJumpForce.x;
            float yVelocity = wallJumpForce.y;

            // Apply the jump force
            rb2d.velocity = new Vector2(xVelocity, yVelocity);

            // Flip the player's direction
            transform.localScale = new Vector3(-xDirection, transform.localScale.y, transform.localScale.z);
        }
    }
}
