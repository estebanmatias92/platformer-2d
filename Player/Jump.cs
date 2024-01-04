using UnityEngine;

namespace Platformer2D.Player
{
    public class Jump : MonoBehaviour
    {
        // Components
        private Rigidbody2D rb2d;

        // Customizable fields
        [SerializeField] private float jumpForce = 16f; // Adjust the jump strengh

        private void Awake()
        {
            // Get components
            rb2d = GetComponent<Rigidbody2D>();
            if (rb2d == null)
                Debug.LogError("Rigidbody2D not found in this GameObject.");
        }

        // Jump execution
        public void executeJump()
        {
            rb2d.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }
}
