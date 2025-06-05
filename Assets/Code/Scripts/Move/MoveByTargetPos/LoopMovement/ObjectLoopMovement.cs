using UnityEngine;

namespace Platformer2D
{
    public abstract class ObjectLoopMovement : ObjectMovementByTargetPos
    {
        protected Vector2 defaultPos;

        protected ObjectLoopMovementConfig loopConfig;

        protected override void Awake()
        {
            base.Awake();

            InitializeMovementLoop();
        }

        protected override void SetConfig()
        {
            base.SetConfig();

            loopConfig = config as ObjectLoopMovementConfig;
        }

        protected virtual void InitializeMovementLoop()
        {
            defaultPos = rb2d.position;
            currentTargetPos = defaultPos + loopConfig.Point_1;
        }

        protected override void CalculateTargetPos()
        {
            if (CheckCanReset())
                ResetMovement();
        }

        protected virtual bool CheckCanReset()
        {
            return Vector2.Distance(rb2d.position, currentTargetPos) <= loopConfig.DistanceThreshole;
        }

        protected abstract void ResetMovement();
    }   
}