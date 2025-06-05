using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D{
    public class KeyCollision : ObjectCollision
    {
        protected ObjectDespawning despawning;

        protected override void Awake()
        {
            base.Awake();

            SetObjectDespawning();
        }

        protected virtual void SetObjectDespawning() => despawning = GetComponentInParent<KeyController>().Despawning;

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
                { HittingObjectType.Player, (collider) => OnTriggerPlayer(collider)},
            };
        }

        protected virtual void OnTriggerPlayer(Collider2D collider)
        {
            despawning.InitializeDespawn();
        }
    }
}