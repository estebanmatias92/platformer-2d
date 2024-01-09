using System;
using UnityEngine;

namespace Platformer2D.Player
{
    public class PlayerCollision : MonoBehaviour
    {
        // Components
        private BoxCollider2D boxCollider;

        // Editor fields
        [SerializeField] private LayerMask wallLayer;

        // Flag
        private bool isGrounded;
        private bool isWalled;

        // Setters
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
        private bool IsPlayerWalled // write-only and private
        {
            set
            {
                if (value != isWalled)
                {
                    isWalled = value;
                    OnWalledStateChanged?.Invoke(isWalled);
                }

            }
        }

        // Events
        public event Action<bool> OnGroundedStateChanged;
        public event Action<bool> OnWalledStateChanged;

        private void Awake()
        {
            if (!TryGetComponent(out boxCollider)) { ShowNullReferenceError(boxCollider); }

            // Utility method just for this scope
            void ShowNullReferenceError(Component component)
            {
                Debug.LogError("[NULL REFERENCE]: " + component.GetType().ToString() + " can't be found");
            }
        }

        private void Update()
        {
            IsPlayerWalled = IsOnWall();
        }

        // MonoBehaviour event hooks are in sync with the FixedUpdate cycle rate
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                IsPlayerGrounded = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                IsPlayerGrounded = false;
            }
        }

        /// <summary>
        /// Checks if the player is touching the wall, uses a boxcast on the side.
        /// </summary>
        /// <returns>
        /// If there is a collider in the way of the raycast, returns true, otherwise false
        /// </returns>
        private bool IsOnWall()
        {
            Vector3 origin = boxCollider.bounds.center;
            Vector3 size = boxCollider.bounds.size;
            float angle = 0f;
            Vector2 direction = new Vector2(transform.localScale.x, 0f);
            float distance = 0.1f;

            // The magic happens here
            RaycastHit2D raycastHit = Physics2D.BoxCast(origin, size, angle, direction, distance, wallLayer);

            bool isHittingCollider = raycastHit.collider != null;

#if UNITY_EDITOR
            // Solo dibuja los rayos si los Gizmos están activados en el Editor
            if (UnityEditor.Selection.Contains(gameObject))
            {
                Color rayColor = (isHittingCollider) ? Color.green : Color.red;
                float x = boxCollider.bounds.extents.x;
                float y = boxCollider.bounds.extents.y;
                Debug.DrawRay(origin + new Vector3(x * direction.x, y, 0), direction * distance, rayColor); // Superior
                Debug.DrawRay(origin - new Vector3(-x * direction.x, y, 0), direction * distance, rayColor); // Inferior
                Debug.DrawRay(origin + new Vector3((x * direction.x) + (distance * direction.x), (-size.y / 2), 0), (Vector2.up * size.y), rayColor); // Lado derecho o izquierdo
            }
#endif

            // Did the raycast hit any collider?
            return isHittingCollider;
        }
    }
}