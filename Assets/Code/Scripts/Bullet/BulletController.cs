using System;
using UnityEngine;

namespace Platformer2D
{
    public class BulletController : MonoBehaviour, IPooled
    {
        [field: SerializeField] public Rigidbody2D Rb2d { get; private set; }
        [field: SerializeField] public ObjectMovement Moving { get; private set; }
        [field: SerializeField] public ObjectCollision Collision { get; private set; }
        [field: SerializeField] public ObjectDespawning Despawning { get; private set; }

        public Action<GameObject> ReleaseCallback { get; set; }

        protected virtual void OnEnable() {
            Despawning.InitializeDespawn();
        }

        protected virtual void OnDisable()
        {
            ReleaseCallback?.Invoke(gameObject);
        }

        protected virtual void FixedUpdate()
        {
            Moving.Move();
        }

        protected virtual void OnTriggerEnter2D(Collider2D collider)
        {
            Collision.HandleTrigger(collider);
        }

        protected virtual void OnCollisionEnter2D(Collision2D collision) {
            Collision.HandleCollision(collision);
        }
    }
}