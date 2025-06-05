namespace Platformer2D
{
    public class Skill
    {
        public Predicate Condition { get; private set; }
        public AttackPattern Pattern { get; private set; }
        public float ResetAttackTime { get; private set; }
        public float CoolDownTime { get; private set; }
        public string SkillClip { get; private set; }

        public Skill(Predicate condition, AttackPattern pattern, float resetAttackTime, float coolDownTime, string skillClip)
        {
            Condition = condition;
            Pattern = pattern;
            ResetAttackTime = resetAttackTime;
            CoolDownTime = coolDownTime;
            SkillClip = skillClip;
        }
    }
}