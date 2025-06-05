using System.Collections;
using UnityEngine;

namespace Platformer2D
{
    public abstract class ObjectDespawnByTime : ObjectDespawning
    {
        [SerializeField] private float aliveTime = 3;

        protected Coroutine despawning;

        public override void InitializeDespawn()
        {
            if (despawning != null)
            {
                StopCoroutine(despawning);
                despawning = null;
            }

            despawning = StartCoroutine(Despawning());
        }

        protected virtual IEnumerator Despawning()
        {
            float currentTime = 0;

            while (currentTime <= aliveTime)
            {
                currentTime += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
                
            Despawn();
        }
    }
}