using System.Collections;
using UnityEngine;

namespace Platformer2D
{
    public class PlayerClimbingState : BasePlayerState
    {
        public PlayerClimbingState(PlayerController playerController, ISPContext<PlayerStateID> context) : base(playerController, context)
        {
            id = PlayerStateID.Climb;
        }

        public override void EnterState(bool playAnim)
        {
            if(playAnim)
                playerController.Animator.Play("Player_Climb");
            
            InputManager.Instance.SwitchToVerticalMap();

            base.EnterState(playAnim);
        }

        public override IEnumerator UpdateState()
        {            
            while (true)
            {
                var climbValue = InputManager.Instance.ClimbValue;
                playerController.Climbing.SetAxisChanged(climbValue.x, climbValue.y);
                playerController.Climbing.Move();

                yield return new WaitForFixedUpdate();
            }
        }

        public override void ExitState()
        {
            playerController.Climbing.Stop();
            InputManager.Instance.SwitchToHorizontalMap();

            base.ExitState();
        }
    }
}