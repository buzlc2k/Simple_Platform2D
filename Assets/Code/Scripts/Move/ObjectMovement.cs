using UnityEngine;

namespace Platformer2D
{
    public abstract class ObjectMovement : MonoBehaviour
    {
        [SerializeField] protected ObjectMovementConfig config;
        protected Rigidbody2D rb2d;

        protected virtual void Awake()
        {
            SetConfig();
            SetRigidbody();
        }

        protected virtual void SetConfig()
        {
            if (config == null) Debug.Log("NULL HERE");
        }

        protected abstract void SetRigidbody();

        protected abstract Vector2 CalculateVelocity();

        public virtual void Move()
        {
            var velo = CalculateVelocity();

            rb2d.linearVelocity = velo;
        }

        public virtual void Stop()
        {
            rb2d.linearVelocity = new(0, 0);
        }
    }   
}