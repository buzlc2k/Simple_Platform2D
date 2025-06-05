using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class ClibingTreeCollision : ObjectCollision
    {
        protected override void InitializeCollisionHandles()
        {
            collisionHandles = new()
            {
            };
        }

        protected override void InitializeTriggerHandles()
        {
            triggerHandles = new()
            {
            };
        }
    }   
}