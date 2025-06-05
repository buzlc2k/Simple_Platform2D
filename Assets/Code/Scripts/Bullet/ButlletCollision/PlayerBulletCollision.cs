using UnityEngine;

namespace Platformer2D{
    public class PlayerBulletCollision : ObjectCollision
    {
        protected ObjectDespawning despawning;

        protected override void Awake()
        {
            base.Awake();

            SetObjectDespawning();
        }

        protected virtual void SetObjectDespawning() => despawning = GetComponentInParent<BulletController>().Despawning;

        protected override void InitializeCollisionHandles()
        {
            // No Colldide
        }

        protected override void InitializeTriggerHandles()
        {
            triggerHandles = new()
            {
                { HittingObjectType.Enemy, (collider) => OnTriggerEnemy(collider) },
            };
        }

        protected virtual void OnTriggerEnemy(Collider2D collider)
        {
            despawning.Despawn();
        }
    }
}