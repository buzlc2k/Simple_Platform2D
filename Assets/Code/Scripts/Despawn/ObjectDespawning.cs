using UnityEngine;

namespace Platformer2D
{
    public abstract class ObjectDespawning : MonoBehaviour
    {

        public virtual void InitializeDespawn()
        {
            Despawn();
        }

        public virtual void Despawn()
        {
            transform.parent.gameObject.SetActive(false);
        }
    }
}