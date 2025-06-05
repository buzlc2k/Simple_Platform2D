namespace Platformer2D
{
    public class EnemySkill : Skill
    {
        public float Priority { get; private set; }
        
        public EnemySkill(Predicate condition, AttackPattern pattern, float resetAttackTime, float coolDownTime, string skillClip, float priority) : base(condition, pattern, resetAttackTime, coolDownTime, skillClip)
        {
            Priority = priority;
        }
    }
}