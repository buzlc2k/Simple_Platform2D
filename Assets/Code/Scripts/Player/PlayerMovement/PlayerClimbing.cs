using UnityEngine;

namespace Platformer2D
{
    public class PlayerClimbing : PlayerMovement
    {
        protected float yAxisChanged = 0;

        protected override void SetRigidbody() => rb2d = GetComponentInParent<PlayerController>().Rb2d;

        public virtual void SetAxisChanged(float axisChanged, float yAxisChanged)
        {
            this.axisChanged = axisChanged;
            this.yAxisChanged = yAxisChanged;
        }

        protected override Vector2 CalculateVelocity()
        {
            return new Vector2(config.Speed * axisChanged, config.Speed * yAxisChanged);
        }
    }
}