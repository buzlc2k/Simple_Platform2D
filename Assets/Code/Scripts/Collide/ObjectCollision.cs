using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
    
namespace Platformer2D{
    public abstract class ObjectCollision : MonoBehaviour
    {
        protected Dictionary<HittingObjectType, Action<Collision2D>> collisionHandles;
        protected Dictionary<HittingObjectType, Action<Collider2D>> triggerHandles;

        [field: SerializeField] public HittingObjectType ObjectType { get; protected set; }

        protected virtual void Awake()
        {
            InitializeCollisionHandles();
            InitializeTriggerHandles();
        }
        
        protected abstract void InitializeCollisionHandles();
        protected abstract void InitializeTriggerHandles();

        public virtual void HandleCollision(Collision2D collision)
        {
            if (!collision.collider.transform.TryGetComponent<ObjectCollision>(out ObjectCollision hittedObject)) return;

            if (collisionHandles.TryGetValue(hittedObject.ObjectType, out Action<Collision2D> actionHandleCollide))
                actionHandleCollide(collision);
        }

        public virtual void HandleTrigger(Collider2D collider)
        {
            if (!collider.transform.TryGetComponent<ObjectCollision>(out ObjectCollision hittedObject)) return;

            if (triggerHandles.TryGetValue(hittedObject.ObjectType, out Action<Collider2D> actionHandleTrigger))
                actionHandleTrigger(collider);
        }
    }
}