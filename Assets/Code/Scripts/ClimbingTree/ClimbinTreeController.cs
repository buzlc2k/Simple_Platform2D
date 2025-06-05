using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class ClimbinTreeController : MonoBehaviour
    {
        // Internal Depedencies
        [field: SerializeField] public ObjectCollision Collision { get; private set; }

        protected virtual void Awake()
        {
            LoadComponents();
        }

        private void LoadComponents()
        {
            if (Collision == null) Collision = GetComponent<ObjectCollision>();
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