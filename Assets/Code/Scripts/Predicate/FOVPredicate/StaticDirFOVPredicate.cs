using UnityEngine;

namespace Platformer2D
{
    public class StaticDirFOVPredicate : FOVPredicate
    {
        protected Vector2 dir;

        public StaticDirFOVPredicate(float dis, LayerMask targetLayer, LayerMask obstacleLayer, Rigidbody2D rb2d, Vector2 dir) : base(dis, targetLayer, obstacleLayer, rb2d)
        {
            this.dir = dir;
        }

        protected override Vector2 GetDirection()
        {
            return dir;
        }
    }
}