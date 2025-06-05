using UnityEngine;

namespace Platformer2D
{
    public class PlayerAttack : ObjectAttacking
    {
        //Internal Depedencies
        private Transform barrel;
        protected Animator animator;

        protected override void Awake()
        {
            SetBarrel();
            SetAnim();

            base.Awake();
        }

        protected virtual void SetBarrel() => barrel = transform.GetChild(0);
        protected virtual void SetAnim() => animator = GetComponentInParent<PlayerController>().Animator;

        protected override void InitializeSkills()
        {
            availableSkills = new();

            Skill basicShooting = new(
                new FuncPredicate(() => InputManager.Instance.AttackTriggered),
                new BasicShootingAttackPattern(gameObject, animator, barrel, BulletID.Player_BasicBullet),
                0.2f,
                0.2f,
                "Player_Attack"
            );

            Skill spread3Shooting = new(
                new FuncPredicate(() => InputManager.Instance.SuperAttackTriggered),
                new SpreadShootingAttackPattern(gameObject, animator, barrel, BulletID.Player_BasicBullet, 3, 10),
                0.2f,
                0.2f,
                "Player_Attack"
            );

            availableSkills.Add(basicShooting);
            availableSkills.Add(spread3Shooting);
        }
    }
}