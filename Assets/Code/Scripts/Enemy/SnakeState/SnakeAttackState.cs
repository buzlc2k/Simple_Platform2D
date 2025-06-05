using System.Collections;
using UnityEngine;

namespace Platformer2D
{
    public class SnakeAttackState : BaseSnakeState
    {
        public SnakeAttackState(SnakeController snakeController, ISPContext<SnakeStateID> context) : base(snakeController, context)
        {
            id = SnakeStateID.Attack;
        }

        public override void EnterState(bool playAnim)
        {
            if (playAnim)
                snakeController.Animator.Play(snakeController.Attacking.CurrentSkill.SkillClip);

            snakeController.ChangingDirection.ChangeDir(Mathf.Sign(snakeController.TargetRb2d.position.x - snakeController.Rb2d.position.x));
            snakeController.Attacking.PerformAttack();

            base.EnterState(playAnim);
        }

        public override IEnumerator UpdateState()
        {
            while (true)
            {
                if (snakeController.Attacking.IsAttacking)
                {
                    yield return new WaitForFixedUpdate();
                    continue;
                }

                if (snakeController.PlayerDetector.Detected)
                    context.SetCurrentState(SnakeStateID.Idel);
                else
                    context.SetCurrentState(SnakeStateID.Move);

                yield break;
            }
        }
    }
}