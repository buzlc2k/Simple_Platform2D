using System.Collections;
using UnityEngine;

namespace Platformer2D
{
    public abstract class BaseSPState<T_ID>
    {
        protected T_ID id;
        protected Coroutine updateState;
        protected ISPContext<T_ID> context;

        #region Properties
        public T_ID ID { get => id; }
        #endregion

        public BaseSPState(ISPContext<T_ID> context)
        {
            this.context = context;
            updateState = null;
        }

        public virtual void EnterState(bool isPriority)
        {
            updateState = GameManager.Instance.StartCoroutine(UpdateState());
        }
        public abstract IEnumerator UpdateState();
        public virtual void ExitState()
        {
            if (updateState == null) return;
            
            GameManager.Instance.StopCoroutine(updateState);
            updateState = null;
        }
    } 
}