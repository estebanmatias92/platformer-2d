using UnityEngine;

namespace Platformer2D.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        // GameObject Components
        private Rigidbody2D rb2d;

        // Editor fields
        [SerializeField] private float moveSpeed = 10f;

        private void Awake()
        {
            rb2d = GetComponent<Rigidbody2D>();
            if (rb2d == null)
                Debug.LogError("Rigidbody2D not found in the object.");
        }

        internal void executeMove(float horizontalInput)
        {
            rb2d.velocity = new Vector2(horizontalInput * moveSpeed, rb2d.velocity.y);
        }
    }
}
