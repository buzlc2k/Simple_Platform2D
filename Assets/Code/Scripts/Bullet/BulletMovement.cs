using UnityEngine;

namespace Platformer2D
{
    public class BulletMovement : ObjectMovement
    {
        protected override void SetRigidbody() => rb2d = GetComponentInParent<BulletController>().Rb2d;

        protected override Vector2 CalculateVelocity()
        {
            return config.Speed * Util.DirectionFromAngle2D(rb2d.rotation);
        }
    }
}