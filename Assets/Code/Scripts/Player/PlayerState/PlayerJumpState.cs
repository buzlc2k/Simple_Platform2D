using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Platformer2D
{
    public class PlayerJumpState : BasePlayerState
    {
        private int currentJumphase = 0;

        public PlayerJumpState(PlayerController playerController, ISPContext<PlayerStateID> context) : base(playerController, context)
        {
            id = PlayerStateID.Jump;
        }

        public override void EnterState(bool playAnim)
        {
            if (playAnim)
                playerController.Animator.Play("Player_Jump");

            playerController.Jumping.Jump();
            currentJumphase++;

            base.EnterState(playAnim);
        }

        public override IEnumerator UpdateState()
        {
            while (playerController.Jumping.IsJumping)
            {
                if (playerController.Attacking.CheckCanAttack())
                    context.AddCurrentState(PlayerStateID.Attack);

                if (InputManager.Instance.JumpTriggered && currentJumphase <= 2)
                {
                    playerController.Jumping.Jump(true);
                    currentJumphase++;
                }

                if (InputManager.Instance.MoveValue != 0)
                    context.AddCurrentState(PlayerStateID.Move);

                yield return new WaitForFixedUpdate();
            }

            context.RemoveCurrentState(this);
        }

        public override void ExitState()
        {
            currentJumphase = 0;

            base.ExitState();
        }
    }
}