using UnityEngine;

namespace Platformer2D
{
    public class SnakePlayerDetector : ObjectDetectorWithObstacle
    {
        private Rigidbody2D targetRb2d;

        protected override void SetRigidbody()
        {
            rb2d = GetComponentInParent<SnakeController>().Rb2d;
            targetRb2d = PlayerController.Instance.Rb2d;
        }

        protected override void SetFOVPredicate()
        {
            fOVPredicate = new TargetFOVPredicate(dis, targetLayer, obstacleLayer, rb2d, targetRb2d);
        }
    }
}