using UnityEngine;

namespace Platformer2D
{
    public class SnakeChasing : ObjectMovementByTargetPos
    {
        private Rigidbody2D targetRb2d;

        protected override void SetRigidbody()
        {
            rb2d = GetComponentInParent<SnakeController>().Rb2d;
            targetRb2d = PlayerController.Instance.Rb2d;
        }

        protected override void CalculateTargetPos()
        {
            currentTargetPos = new(targetRb2d.position.x, rb2d.position.y);
        }
    }
}