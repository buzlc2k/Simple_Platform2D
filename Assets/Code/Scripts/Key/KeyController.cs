using UnityEngine;

namespace Platformer2D
{
    public class KeyController : MonoBehaviour
    {
        // Internal Depedencies
        [field: SerializeField] public ObjectCollision Collision { get; private set; }
        [field: SerializeField] public ObjectDespawning Despawning { get; private set; }

        protected virtual void Awake()
        {
            LoadComponents();
        }

        private void LoadComponents()
        {
            if (Collision == null) Collision = GetComponentInChildren<ObjectCollision>();
            if (Despawning == null) Despawning = GetComponentInChildren<ObjectDespawning>();
        }

        protected virtual void OnTriggerEnter2D(Collider2D collider)
        {
            Collision.HandleTrigger(collider);
        }
    }
}