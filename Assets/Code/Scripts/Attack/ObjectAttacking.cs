using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

namespace Platformer2D
{
    public abstract class ObjectAttacking : MonoBehaviour
    {
        protected bool canAttack = true;
        protected bool isAttacking = false;
        protected Skill currentSkill;
        protected List<Skill> availableSkills;

        #region Properties
        public bool CanAttack { get => canAttack; }
        public bool IsAttacking { get => isAttacking; }
        public Skill CurrentSkill { get => currentSkill; }
        #endregion

        protected virtual void Awake()
        {
            InitializeSkills();
        }

        protected abstract void InitializeSkills();

        public virtual bool CheckCanAttack()
        {
            if (availableSkills == null || availableSkills.Count == 0) return false;

            var selectedSkill = availableSkills.FirstOrDefault(skill => skill.Condition.Evaluate());
            if (selectedSkill == null) return false;

            currentSkill = selectedSkill;
            return true;
        }

        public virtual void PerformAttack()
        {
            StartCoroutine(currentSkill.Pattern.PerformAttack(() => StopAttack()));

            isAttacking = true;
            canAttack = false;
        }

        public virtual void StopAttack()
        {
            isAttacking = false;

            StartCoroutine(ResetAttack());
            StartCoroutine(CoolDownSkill());
        }

        protected virtual IEnumerator ResetAttack()
        {
            float currentTime = 0;
            float resetTime = currentSkill.ResetAttackTime;

            while (currentTime < resetTime && gameObject.activeInHierarchy)
            {
                currentTime += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            canAttack = true;
        }

        protected virtual IEnumerator CoolDownSkill()
        {
            var coolingDownSkill = currentSkill;
            availableSkills.Remove(currentSkill);

            float currentTime = 0;

            while (currentTime < coolingDownSkill.CoolDownTime && gameObject.activeInHierarchy)
            {
                currentTime += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            availableSkills.Add(coolingDownSkill);
        }
    }
}