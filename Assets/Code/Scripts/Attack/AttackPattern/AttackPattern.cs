using System;
using System.Collections;
using UnityEngine;

namespace Platformer2D
{
    public abstract class AttackPattern
    {
        protected GameObject objectAttacked;

        public AttackPattern(GameObject objectAttacked)
        {
            this.objectAttacked = objectAttacked;
        }

        public abstract IEnumerator PerformAttack(Action stopAttack);
    }   
}