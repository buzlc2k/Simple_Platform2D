using System;
using System.Collections;
using UnityEngine;

namespace Platformer2D
{
    public class SnakeMoveState : BaseSnakeState
    {
        private readonly float maxTimeToChangeIdelState = 3;

        public SnakeMoveState(SnakeController snakeController, ISPContext<SnakeStateID> context) : base(snakeController, context)
        {
            id = SnakeStateID.Move;
        }

        public override void EnterState(bool playAnim)
        {
            if (playAnim)
                snakeController.Animator.Play("Snake_Move");

            base.EnterState(playAnim);
        }

        public override IEnumerator UpdateState()
        {
            float timeToChangeIdelState = UnityEngine.Random.Range(1, maxTimeToChangeIdelState);
            
            while (true)
            {
                snakeController.Moving.Move();
                timeToChangeIdelState -= Time.fixedDeltaTime;
                snakeController.ChangingDirection.ChangeDir(MathF.Sign(snakeController.Rb2d.linearVelocityX));

                if (snakeController.PlayerDetector.Detected)
                {
                    context.SetCurrentState(SnakeStateID.Chase);
                    yield break;
                }

                if (timeToChangeIdelState <= 0)
                {
                    context.SetCurrentState(SnakeStateID.Idel);
                    yield break;
                }

                yield return new WaitForFixedUpdate();
            }
        }

        public override void ExitState()
        {
            snakeController.Moving.Stop();

            base.ExitState();
        }
    }
}