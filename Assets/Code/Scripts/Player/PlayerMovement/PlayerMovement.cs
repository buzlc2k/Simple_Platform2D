using UnityEngine;

namespace Platformer2D
{
    public abstract class PlayerMovement : ObjectMovement
    {
        protected float axisChanged = 0;

        protected override void SetRigidbody() => rb2d = GetComponentInParent<PlayerController>().Rb2d;

        public virtual void SetAxisChanged(float axisChanged)
        {
            this.axisChanged = axisChanged;
        }
    }
}