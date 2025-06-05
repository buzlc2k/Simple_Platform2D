using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class FrogAttack : SnakeAttack
    {
        protected ObjectDespawning despawning;

        protected override void Awake()
        {
            SetObjectDespawning();

            base.Awake();
        }

        protected virtual void SetObjectDespawning() => despawning = GetComponentInParent<SnakeController>().Despawning;

        protected override void InitializeSkills()
        {
            availableSkills = new();

            EnemySkill frogExplose = new(
                new TargetFOVPredicate(1.25f, 1 << 6, 1 << 8, rb2d, targetRb2d),
                new ExplosionAttackPattern(gameObject, animator, 0.55f, 25, 5f, rb2d, targetRb2d, despawning),
                1.5f,
                1.5f,
                "Snake_Attack",
                0
            );

            availableSkills.Add(frogExplose);
        }
    }
}