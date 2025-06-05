using System;
using System.Collections;
using UnityEngine;

namespace Platformer2D
{
    public class AnimationAttackPattern : AttackPattern
    {
        protected Animator anim;

        public AnimationAttackPattern(GameObject objectAttacked, Animator anim) : base(objectAttacked)
        {
            this.anim = anim;
        }

        protected virtual IEnumerator PlayAttackAnimation()
        {
            yield return null;

            float animTime = anim.GetCurrentAnimatorClipInfo(0)[0].clip.length;

            float currentTime = 0;

            while (currentTime <= animTime && objectAttacked.activeInHierarchy)
            {
                currentTime += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
        }

        public override IEnumerator PerformAttack(Action stopAttack)
        {
            yield return GameManager.Instance.StartCoroutine(PlayAttackAnimation());

            stopAttack();
        }
    }
}