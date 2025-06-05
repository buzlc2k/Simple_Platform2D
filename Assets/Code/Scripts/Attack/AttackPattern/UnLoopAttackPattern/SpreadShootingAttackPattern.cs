using System;
using System.Collections;
using UnityEngine;

namespace Platformer2D
{
    public class SpreadShootingAttackPattern : BasicShootingAttackPattern
    {
        protected readonly float numDir;
        protected readonly float offsetAngle;

        public SpreadShootingAttackPattern(GameObject objectAttacked, Animator anim, Transform barrel, BulletID bulletID, float numDir, float offsetAngle) : base(objectAttacked, anim, barrel, bulletID)
        {
            this.numDir = numDir;
            this.offsetAngle = offsetAngle;
        }

        public override IEnumerator PerformAttack(Action stopAttack)
        {

            for (int i = 1; i < (numDir / 2); i++)
            {
                float currentOffsetAngle = offsetAngle * i;

                bulletFactory.Get(bulletID, barrel.position, Quaternion.Euler(0, 0, currentOffsetAngle) * barrel.rotation);
                bulletFactory.Get(bulletID, barrel.position, Quaternion.Euler(0, 0, -currentOffsetAngle) * barrel.rotation);
            }

            yield return GameManager.Instance.StartCoroutine(base.PerformAttack(stopAttack));
        }
    }
}