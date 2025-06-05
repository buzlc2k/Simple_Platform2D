using UnityEngine;

namespace Platformer2D
{
    public class PlayerMoveBasic : PlayerMovement
    {
        protected override void SetRigidbody() => rb2d = GetComponentInParent<PlayerController>().Rb2d;

        protected override Vector2 CalculateVelocity()
        {
            return new Vector2(config.Speed * axisChanged, rb2d.linearVelocityY);
        }

        public override void Stop()
        {
            rb2d.linearVelocity = new(0, rb2d.linearVelocityY);
        }
    }
}