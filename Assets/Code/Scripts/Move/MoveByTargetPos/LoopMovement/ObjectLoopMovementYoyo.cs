using UnityEngine;

namespace Platformer2D
{
    public abstract class ObjectLoopMovementYoyo : ObjectLoopMovement
    {
        protected override void ResetMovement()
        {
            var pointAdded = currentTargetPos == defaultPos + loopConfig.Point_1 ? loopConfig.Point_2 : loopConfig.Point_1;

            currentTargetPos = defaultPos + pointAdded;
        }
    }
}