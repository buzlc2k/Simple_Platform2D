using System.Collections;
using UnityEngine;

namespace Platformer2D
{
    public class PlayerAttackState : BasePlayerState
    {
        public PlayerAttackState(PlayerController playerController, ISPContext<PlayerStateID> context) : base(playerController, context)
        {
            id = PlayerStateID.Attack;
        }

        public override void EnterState(bool playAnim)
        {
            if (playAnim)
                playerController.Animator.Play(playerController.Attacking.CurrentSkill.SkillClip);

            playerController.Attacking.PerformAttack();

            base.EnterState(playAnim);
        }

        public override IEnumerator UpdateState()
        {
            while (playerController.Attacking.IsAttacking)
            {
                if (InputManager.Instance.MoveValue != 0)
                    context.AddCurrentState(PlayerStateID.Move);

                if(InputManager.Instance.JumpTriggered)
                    context.AddCurrentState(PlayerStateID.Jump);

                yield return new WaitForFixedUpdate();
            }

            context.RemoveCurrentState(this);
        }
    }
}