using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D{
    public class SnakeCollision : ObjectCollision
    {
        protected Rigidbody2D rb2d;
        protected ObjectDespawning despawning;

        protected override void Awake()
        {
            base.Awake();

            SetRigidbody();
            SetObjectDespawning();
        }

        protected virtual void SetRigidbody() => rb2d = GetComponentInParent<SnakeController>().Rb2d;
        protected virtual void SetObjectDespawning() => despawning = GetComponentInParent<SnakeController>().Despawning;

        protected override void InitializeCollisionHandles()
        {
            //Khởi tạo Dictionary
            collisionHandles = new()
            {
                //Thêm các phần tử vào Dictionary
                { HittingObjectType.Player, (collision) => OnCollidePlayer(collision) },
            };
        }

        protected override void InitializeTriggerHandles()
        {
            triggerHandles = new()
            {
                { HittingObjectType.Player_Bullet, (collider) => OnTriggerPlayerBullet(collider) },
            };
        }

        protected virtual void OnTriggerPlayerBullet(Collider2D collider)
        {
            despawning.InitializeDespawn();
        }

        protected virtual void OnCollidePlayer(Collision2D collision)
        {
            ContactPoint2D contact = collision.GetContact(0);
            Vector2 colDir = (contact.point - rb2d.position).normalized;

            if (Vector2.Dot(colDir, Vector2.up) < 0.1f) return;
            
            despawning.InitializeDespawn();
        } 
    }
}