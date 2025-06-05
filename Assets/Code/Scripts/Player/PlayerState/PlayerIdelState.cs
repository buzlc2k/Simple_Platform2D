using System.Collections;
using UnityEngine;

namespace Platformer2D
{
    public class PlayerIdelState : BasePlayerState
    {
        public PlayerIdelState(PlayerController playerController, ISPContext<PlayerStateID> context) : base(playerController, context)
        {
            id = PlayerStateID.Idel;
        }

        public override void EnterState(bool playAnim)
        {
            if(playAnim)
                playerController.Animator.Play("Player_Idel");

            base.EnterState(playAnim);
        }

        public override IEnumerator UpdateState()
        {
            while (true)
            {
                if (playerController.Attacking.CheckCanAttack())
                {
                    context.SetCurrentState(PlayerStateID.Attack);
                    yield break;   
                }

                if (InputManager.Instance.MoveValue != 0)
                {
                    context.SetCurrentState(PlayerStateID.Move);
                    yield break;
                }

                if (InputManager.Instance.JumpTriggered)
                {
                    context.SetCurrentState(PlayerStateID.Jump);
                    yield break;
                }
            
                yield return new WaitForFixedUpdate();
            }
        }
    }
}