using UnityEngine;

namespace Platformer2D
{
    public abstract class ObjectMovementByTargetPos : ObjectMovement
    {
        protected Vector2 currentTargetPos;

        protected abstract void CalculateTargetPos();

        protected override Vector2 CalculateVelocity()
        {
            CalculateTargetPos();

            var dir = (currentTargetPos - rb2d.position).normalized;

            return dir * config.Speed;
        }
    }   
}