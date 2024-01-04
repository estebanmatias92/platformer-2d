using UnityEngine;

namespace Platformer2D.Player
{
    public class Collision : MonoBehaviour
    {

        private bool isOnGround = false;

        public bool IsPlayerOnGround
        {
            get { return isOnGround; }
            private set { isOnGround = value; }
        }

        private void Start()
        {
            IsPlayerOnGround = false;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                IsPlayerOnGround = true;
            }
        }


        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                IsPlayerOnGround = false;
            }
        }

    }
}