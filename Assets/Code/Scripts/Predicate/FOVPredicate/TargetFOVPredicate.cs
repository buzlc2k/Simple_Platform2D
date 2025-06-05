using UnityEngine;

namespace Platformer2D
{
    public class TargetFOVPredicate : FOVPredicate
    {
        protected Rigidbody2D targetRb2d;

        public TargetFOVPredicate(float dis, LayerMask targetLayer, LayerMask obstacleLayer, Rigidbody2D rb2d, Rigidbody2D targetRb2d) : base(dis, targetLayer, obstacleLayer, rb2d)
        {
            this.targetRb2d = targetRb2d;
        }

        protected override Vector2 GetDirection()
        {
            return (targetRb2d.position - rb2d.position).normalized;
        }
    }
}