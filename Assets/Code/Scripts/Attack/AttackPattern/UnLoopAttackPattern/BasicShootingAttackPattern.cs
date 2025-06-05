using System;
using System.Collections;
using UnityEngine;

namespace Platformer2D
{
    public class BasicShootingAttackPattern : AnimationAttackPattern
    {
        protected Transform barrel;
        protected readonly BulletID bulletID;

        protected readonly BulletFactory bulletFactory;

        public BasicShootingAttackPattern(GameObject objectAttacked, Animator anim, Transform barrel, BulletID bulletID) : base(objectAttacked, anim)
        {
            this.barrel = barrel;
            this.bulletID = bulletID;

            bulletFactory = FactoryManager.Instance.BulletFactory;
        }

        public override IEnumerator PerformAttack(Action stopAttack)
        {
            bulletFactory.Get(bulletID, barrel.position, barrel.rotation);

            yield return GameManager.Instance.StartCoroutine(base.PerformAttack(stopAttack));
        }
    }
}