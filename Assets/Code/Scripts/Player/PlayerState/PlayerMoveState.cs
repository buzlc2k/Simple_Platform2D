using System.Collections;
using UnityEngine;

namespace Platformer2D
{
    public class PlayerMoveState : BasePlayerState
    {
        public PlayerMoveState(PlayerController playerController, ISPContext<PlayerStateID> context) : base(playerController, context)
        {
            id = PlayerStateID.Move;
        }

        public override void EnterState(bool playAnim)
        {
            if(playAnim)
                playerController.Animator.Play("Player_Movement");

            base.EnterState(playAnim);
        }

        public override IEnumerator UpdateState()
        {            
            while (true)
            {
                if (InputManager.Instance.MoveValue == 0)
                {
                    if (!playerController.Attacking.IsAttacking && !playerController.Jumping.IsJumping)
                    {
                        context.SetCurrentState(PlayerStateID.Idel);
                        yield break;
                    }
                    
                    playerController.Movement.Stop();
                }
                else
                {
                    playerController.ChangingDirection.ChangeDir(InputManager.Instance.MoveValue);
                    playerController.Movement.SetAxisChanged(InputManager.Instance.MoveValue);
                    playerController.Movement.Move();
                }
                
                if(playerController.Attacking.CheckCanAttack())
                    context.AddCurrentState(PlayerStateID.Attack);
                
                if (InputManager.Instance.JumpTriggered)
                    context.AddCurrentState(PlayerStateID.Jump);

                yield return new WaitForFixedUpdate();
            }
        }

        public override void ExitState()
        {
            playerController.Movement.Stop();

            base.ExitState();
        }
    }
}