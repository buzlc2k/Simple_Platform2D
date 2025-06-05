using UnityEngine;

namespace Platformer2D
{
    public abstract class ObjectDetector : MonoBehaviour
    {
        [SerializeField] protected float dis = 1f;
        [SerializeField] protected LayerMask targetLayer;

        protected Rigidbody2D rb2d;

        protected FOVPredicate fOVPredicate;

        #region Properties
        public bool Detected { get
            {
                if (fOVPredicate == null) return false;
                return fOVPredicate.Evaluate();
            } }
        #endregion

        protected virtual void Awake()
        {
            SetRigidbody();
            SetFOVPredicate();
        }

        protected abstract void SetRigidbody();
        
        protected abstract void SetFOVPredicate();
    }
}