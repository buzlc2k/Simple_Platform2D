using UnityEngine;

namespace Platformer2D
{
    public abstract class FOVPredicate : Predicate
    {
        protected float dis = 1f;
        protected LayerMask targetLayer;
        protected LayerMask obstacleLayer;

        protected Rigidbody2D rb2d;

        public FOVPredicate(float dis, LayerMask targetLayer, LayerMask obstacleLayer, Rigidbody2D rb2d)
        {
            this.dis = dis;
            this.targetLayer = targetLayer;
            this.obstacleLayer = obstacleLayer;
            this.rb2d = rb2d;
        }

        protected abstract Vector2 GetDirection();

        public override bool Evaluate()
        {
            RaycastHit2D hit = Physics2D.Raycast(rb2d.position, GetDirection(), dis, targetLayer + obstacleLayer);
            
            return hit && ((1 << hit.collider.gameObject.layer) & targetLayer) != 0;
        }
    }
}