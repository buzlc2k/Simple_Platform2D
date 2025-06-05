using UnityEngine;

namespace Platformer2D
{
    public class SnakeMovement : ObjectLoopMovementYoyo
    {
        protected override void SetRigidbody() => rb2d = GetComponentInParent<SnakeController>().Rb2d;
    }
}