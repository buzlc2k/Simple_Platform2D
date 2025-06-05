using System;
using System.Collections;
using UnityEngine;

namespace Platformer2D
{
    public class ExplosionAttackPattern : AnimationAttackPattern
    {
        protected float offsetTimeToExplose;
        protected float exploseForce;
        protected float exploseRadius;
        protected ObjectDespawning despawning;
        protected Rigidbody2D rb2d;
        protected Rigidbody2D targetRb2d;

        public ExplosionAttackPattern(GameObject objectAttacked, Animator anim, float offsetTimeToExplose, float exploseForce, float exploseRadius, Rigidbody2D rb2d, Rigidbody2D targetRb2d, ObjectDespawning despawning) : base(objectAttacked, anim)
        {
            this.offsetTimeToExplose = offsetTimeToExplose;
            this.exploseForce = exploseForce;
            this.exploseRadius = exploseRadius;
            this.rb2d = rb2d;
            this.targetRb2d = targetRb2d;
            this.despawning = despawning;
        }

        protected override IEnumerator PlayAttackAnimation()
        {
            yield return null;

            float animTime = anim.GetCurrentAnimatorClipInfo(0)[0].clip.length;

            float currentTime = 0;
            bool explosed = false;

            while (currentTime <= animTime && objectAttacked.activeInHierarchy)
            {
                currentTime += Time.fixedDeltaTime;
                if (currentTime >= offsetTimeToExplose && !explosed)
                {
                    Explose();
                    explosed = true;
                }

                yield return new WaitForFixedUpdate();
            }
        }

        public override IEnumerator PerformAttack(Action stopAttack)
        {
            yield return GameManager.Instance.StartCoroutine(base.PerformAttack(stopAttack));

            despawning.InitializeDespawn();
        }

        protected virtual void Explose()
        {
            Vector2 relativeVector = targetRb2d.position - rb2d.position;
            float dis = relativeVector.magnitude;

            if (dis > exploseRadius) return;

            float normalizedDistance = 1f - (dis / exploseRadius);
            float appliedForce = normalizedDistance * exploseForce;
            Vector2 exploseDir = relativeVector.normalized;

            targetRb2d.AddForce(exploseDir * appliedForce, ForceMode2D.Impulse);
        }
    }
}