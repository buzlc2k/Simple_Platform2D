using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Platformer2D
{
    public class SnakeAttack : ObjectAttacking
    {
        //Internal Depedencies
        protected Animator animator;
        protected Rigidbody2D rb2d;

        //External Depedencies
        protected Rigidbody2D targetRb2d;

        protected override void Awake()
        {
            SetAnim();
            SetRigidbody();
            SetTargetRb2d();

            base.Awake();
        }

        protected virtual void SetAnim() => animator = GetComponentInParent<SnakeController>().Animator;
        protected virtual void SetRigidbody() => rb2d = GetComponentInParent<SnakeController>().Rb2d;
        protected virtual void SetTargetRb2d() => targetRb2d = PlayerController.Instance.Rb2d;

        protected override void InitializeSkills()
        {
            availableSkills = new();

            EnemySkill snakeBite = new(
                new TargetFOVPredicate(0.65f, 1 << 6, 1 << 8, rb2d, targetRb2d),
                new AnimationAttackPattern(gameObject, animator),
                1.5f,
                1.5f,
                "Snake_Attack",
                0
            );

            availableSkills.Add(snakeBite);
        }

        public override bool CheckCanAttack()
        {
            if (availableSkills.Count == 0) return false;

            currentSkill = availableSkills
                .OfType<EnemySkill>()
                .Where(skill => skill.Condition.Evaluate())
                .OrderByDescending(skill => skill.Priority)
                .FirstOrDefault();

            return currentSkill != null;
        }
    }
}