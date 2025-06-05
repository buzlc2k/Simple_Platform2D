using System.Collections;
using UnityEngine;

namespace Platformer2D
{
    public class PlayerJumping : MonoBehaviour
    {
        private bool isJumping = false;
        private Coroutine resetJumping = null;
        
        [SerializeField] private float jumpForce = 6.5f;
        [SerializeField] private float fallMultiplier = 3.5f;

        private readonly float globalGravity = -9.81f;

        private Rigidbody2D rb2d;
        private ObjectDetector groundDetector;

        #region Properties
        public bool IsJumping { get => isJumping; }
        #endregion

        private void Awake()
        {
            SetRigidbody();
            SetGroundDetector();
        }

        private void FixedUpdate()
        {
            AddPlayerFallGravity();
        }

        private void SetRigidbody() => rb2d = GetComponentInParent<PlayerController>().Rb2d;

        private void SetGroundDetector() => groundDetector = GetComponentInParent<PlayerController>().GroundDetector;

        public void Jump(bool forceJump = false)
        {
            if (!forceJump && isJumping) return;

            isJumping = true;

            rb2d.linearVelocity = new(rb2d.linearVelocityX, 0);
            rb2d.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);

            resetJumping ??= StartCoroutine(ResetJump());
        }

        private void AddPlayerFallGravity()
        {
            if (rb2d.linearVelocityY < 0)
                rb2d.linearVelocity += fallMultiplier * globalGravity * Time.fixedDeltaTime * Vector2.up;
        }

        private IEnumerator ResetJump()
        {
            while (!groundDetector.Detected || rb2d.linearVelocityY > 0)
                yield return new WaitForSeconds(Time.fixedDeltaTime);

            isJumping = false;
            resetJumping = null;
        }
    }
}