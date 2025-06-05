using System;
using System.Collections;
using UnityEngine;

namespace Platformer2D
{
    public class SnakeChaseState : BaseSnakeState
    {
        public SnakeChaseState(SnakeController snakeController, ISPContext<SnakeStateID> context) : base(snakeController, context)
        {
            id = SnakeStateID.Chase;
        }

        public override void EnterState(bool playAnim)
        {
            if (playAnim)
                snakeController.Animator.Play("Snake_Move");

            base.EnterState(playAnim);
        }

        public override IEnumerator UpdateState()
        {
            while (true)
            {
                if (snakeController.Attacking.CheckCanAttack())
                {
                    context.SetCurrentState(SnakeStateID.Attack);
                    yield break;
                }

                if (!snakeController.PlayerDetector.Detected)
                {
                    context.SetCurrentState(SnakeStateID.Move);
                    yield break;
                }

                snakeController.Chasing.Move();
                snakeController.ChangingDirection.ChangeDir(MathF.Sign(snakeController.Rb2d.linearVelocityX));
                
                yield return new WaitForFixedUpdate();
            }
        }

        public override void ExitState()
        {
            snakeController.Chasing.Stop();

            base.ExitState();
        }
    }
}