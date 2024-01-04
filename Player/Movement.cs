using UnityEngine;

namespace Platformer2D.Player
{
    public class Movement : MonoBehaviour
    {
        /// Components
        private Rigidbody2D rb2d;

        // Customizable fields
        [SerializeField] private float moveSpeed = 10f;

        // Properties
        float movementThreshold = 0.01f;
        private bool isStill;
        public bool IsPlayerStill
        {
            get { return IsNotMoving(); }
            private set { isStill = value; }
        }

        private void Awake()
        {
            rb2d = GetComponent<Rigidbody2D>();
            if (rb2d == null)
                Debug.LogError("Rigidbody2D not found in the object.");
        }

        private void Start()
        {
            IsPlayerStill = true;
        }

        public void executeMove(float horizontalMovement = 0f)
        {
            rb2d.velocity = new Vector2(horizontalMovement * moveSpeed, rb2d.velocity.y);
        }

        private bool IsNotMoving()
        {
            return Mathf.Abs(rb2d.velocity.x) < movementThreshold;
        }
    }
}
